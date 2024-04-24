using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DataTableGenerator;
using UnityEngine;

namespace DataTableGenerator
{
    /// <summary>
    /// 通过数据表生成代码的核心逻辑类
    /// </summary>
    public sealed class DataTableGenerator
    {
        //配置表文件所在路径
        private const string DataTablePath = "Assets/GameMain/DataTables";
        //生成的C#代码路径
        private const string CSharpCodePath = "Assets/GameMain/Scripts/DataTable";
        //C#代码模板
        private const string CSharpCodeTemplateFileName = "Assets/GameMain/Configs/DataTableCodeTemplate.txt";
        //C#代码文件前缀
        private const string CSharpFilePrefix = "DATA";
        //C#代码文件命名空间
        private const string CSharpCodeNamespace = "FightingGame";
        private static readonly Regex EndWithNumberRegex = new Regex(@"\d+$");
        private static readonly Regex NameRegex = new Regex(@"^[A-Z][A-Za-z0-9_]*$");

        public static DataTableProcessor CreateDataTableProcessor(string dataTableName)
        {
            return new DataTableProcessor(Utility.Path.GetRegularPath(Path.Combine(DataTablePath, dataTableName + ".txt")), Encoding.GetEncoding("GB2312"), 1, 2, null, 3, 4, 1);
        }

        public static bool CheckRawData(DataTableProcessor dataTableProcessor, string dataTableName)
        {
            for (int i = 0; i < dataTableProcessor.RawColumnCount; i++)
            {
                string name = dataTableProcessor.GetName(i);
                if (string.IsNullOrEmpty(name) || name == "#")
                {
                    continue;
                }

                if (!NameRegex.IsMatch(name))
                {
                    Debug.LogWarning(Utility.Text.Format("Check raw data failure. DataTableName='{0}' Name='{1}'", dataTableName, name));
                    return false;
                }
            }

            return true;
        }

        public static void GenerateDataFile(DataTableProcessor dataTableProcessor, string dataTableName)
        {
            string binaryDataFileName = Utility.Path.GetRegularPath(Path.Combine(DataTablePath, dataTableName + ".bytes"));
            if (!dataTableProcessor.GenerateDataFile(binaryDataFileName) && File.Exists(binaryDataFileName))
            {
                File.Delete(binaryDataFileName);
            }
        }

        public static void GenerateCodeFile(DataTableProcessor dataTableProcessor, string dataTableName)
        {
            dataTableProcessor.SetCodeTemplate(CSharpCodeTemplateFileName, Encoding.UTF8);
            dataTableProcessor.SetCodeGenerator(DataTableCodeGenerator);

            string csharpCodeFileName = Utility.Path.GetRegularPath(Path.Combine(CSharpCodePath, $"{CSharpFilePrefix}{dataTableName}.cs"));
            if (!dataTableProcessor.GenerateCodeFile(csharpCodeFileName, Encoding.UTF8, dataTableName) && File.Exists(csharpCodeFileName))
            {
                File.Delete(csharpCodeFileName);
            }
        }

        private static void DataTableCodeGenerator(DataTableProcessor dataTableProcessor, StringBuilder codeContent, object userData)
        {
            string dataTableName = (string)userData;

            codeContent.Replace("__DATA_TABLE_CREATE_TIME__", DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"));
            codeContent.Replace("__DATA_TABLE_NAME_SPACE__", CSharpCodeNamespace);
            codeContent.Replace("__DATA_TABLE_CLASS_NAME__", $"{CSharpFilePrefix}{dataTableName}");
            codeContent.Replace("__DATA_TABLE_COMMENT__", dataTableProcessor.GetValue(0, 1) + "。");
            codeContent.Replace("__DATA_TABLE_ID_COMMENT__", "获取" + dataTableProcessor.GetComment(dataTableProcessor.IdColumn) + "。");
            codeContent.Replace("__DATA_TABLE_PROPERTIES__", GenerateDataTableProperties(dataTableProcessor));
            codeContent.Replace("__DATA_TABLE_PARSER__", GenerateDataTableParser(dataTableProcessor));
            codeContent.Replace("__DATA_TABLE_PROPERTY_ARRAY__", GenerateDataTablePropertyArray(dataTableProcessor));
        }

        private static string GenerateDataTableProperties(DataTableProcessor dataTableProcessor)
    {
        StringBuilder stringBuilder = new StringBuilder();
        bool firstProperty = true;
        for (int i = 0; i < dataTableProcessor.RawColumnCount; i++)
        {
            if (dataTableProcessor.IsCommentColumn(i))
            {
                // 注释列
                continue;
            }

            if (dataTableProcessor.IsIdColumn(i))
            {
                // 编号列
                continue;
            }

            if (firstProperty)
            {
                firstProperty = false;
            }
            else
            {
                stringBuilder.AppendLine().AppendLine();
            }

            stringBuilder
                .AppendLine("\t\t/// <summary>")
                .AppendFormat("\t\t/// 获取{0}。", dataTableProcessor.GetComment(i)).AppendLine()
                .AppendLine("\t\t/// </summary>")
                .AppendFormat("\t\tpublic {0} {1}", dataTableProcessor.GetLanguageKeyword(i),
                    dataTableProcessor.GetName(i)).AppendLine()
                .AppendLine("\t\t{")
                .AppendLine("\t\t\tget;")
                .AppendLine("\t\t\tprivate set;")
                .Append("\t\t}");
        }

        return stringBuilder.ToString();
    }

    private static string GenerateDataTableParser(DataTableProcessor dataTableProcessor)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder
            .AppendLine("\t\tpublic override bool ParseDataRow(string dataRowString, object userData)")
            .AppendLine("\t\t{")
            .AppendLine(
                "\t\t\tstring[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);")
            .AppendLine("\t\t\tfor (int i = 0; i < columnStrings.Length; i++)")
            .AppendLine("\t\t\t{")
            .AppendLine(
                "\t\t\t\tcolumnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);")
            .AppendLine("\t\t\t}")
            .AppendLine()
            .AppendLine("\t\t\tint index = 0;");

        for (int i = 0; i < dataTableProcessor.RawColumnCount; i++)
        {
            if (dataTableProcessor.IsCommentColumn(i))
            {
                // 注释列
                stringBuilder.AppendLine("\t\t\tindex++;");
                continue;
            }

            if (dataTableProcessor.IsIdColumn(i))
            {
                // 编号列
                stringBuilder.AppendLine("\t\t\tm_Id = int.Parse(columnStrings[index++]);");
                continue;
            }

            //只有 C# 提供的类才是系统类
            if (dataTableProcessor.IsSystem(i))
            {
                string languageKeyword = dataTableProcessor.GetLanguageKeyword(i);
                if (languageKeyword == "string")
                {
                    stringBuilder
                        .AppendFormat("\t\t\t{0} = columnStrings[index++];", dataTableProcessor.GetName(i))
                        .AppendLine();
                }
                else
                {
                    stringBuilder.AppendFormat("\t\t\t{0} = {1}.Parse(columnStrings[index++]);",
                        dataTableProcessor.GetName(i), languageKeyword).AppendLine();
                }
            }
            else
            {
                //添加自定义的数据类型
                if (dataTableProcessor.GetLanguageKeyword(i) == "List<Vector2>")
                {
                    stringBuilder.AppendFormat("\t\t\t{0} = DataTableExtension.Parse{1}(columnStrings[index++]);",
                        dataTableProcessor.GetName(i), "Vector2List").AppendLine();
                }
                else
                {
                    stringBuilder.AppendFormat("\t\t\t{0} = DataTableExtension.Parse{1}(columnStrings[index++]);",
                        dataTableProcessor.GetName(i), dataTableProcessor.GetType(i).Name).AppendLine();
                }
            }
        }

        stringBuilder.AppendLine()
            .AppendLine("\t\t\tGeneratePropertyArray();")
            .AppendLine("\t\t\treturn true;")
            .AppendLine("\t\t}")
            .AppendLine()
            .AppendLine(
                "\t\tpublic override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)")
            .AppendLine("\t\t{")
            .AppendLine(
                "\t\t\tusing (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))")
            .AppendLine("\t\t\t{")
            .AppendLine(
                "\t\t\t\tusing (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))")
            .AppendLine("\t\t\t\t{");

        for (int i = 0; i < dataTableProcessor.RawColumnCount; i++)
        {
            if (dataTableProcessor.IsCommentColumn(i))
            {
                // 注释列
                continue;
            }

            if (dataTableProcessor.IsIdColumn(i))
            {
                // 编号列
                stringBuilder.AppendLine("\t\t\t\t\tm_Id = binaryReader.Read7BitEncodedInt32();");
                continue;
            }

            string languageKeyword = dataTableProcessor.GetLanguageKeyword(i);
            if (languageKeyword == "int" || languageKeyword == "uint" || languageKeyword == "long" ||
                languageKeyword == "ulong")
            {
                stringBuilder.AppendFormat("\t\t\t\t\t{0} = binaryReader.Read7BitEncoded{1}();",
                    dataTableProcessor.GetName(i), dataTableProcessor.GetType(i).Name).AppendLine();
            }
            else
            {
                //添加自定义的数据类型
                if (dataTableProcessor.GetLanguageKeyword(i) == "List<Vector2>")
                {
                    stringBuilder.AppendFormat("\t\t\t\t\t{0} = binaryReader.Read{1}();",
                        dataTableProcessor.GetName(i), "Vector2List").AppendLine();
                }
                else
                {
                    stringBuilder.AppendFormat("\t\t\t\t\t{0} = binaryReader.Read{1}();",
                        dataTableProcessor.GetName(i), dataTableProcessor.GetType(i).Name).AppendLine();
                }
            }
        }

        stringBuilder
            .AppendLine("\t\t\t\t}")
            .AppendLine("\t\t\t}")
            .AppendLine()
            .AppendLine("\t\t\tGeneratePropertyArray();")
            .AppendLine("\t\t\treturn true;")
            .Append("\t\t}");

        return stringBuilder.ToString();
    }

    private static string GenerateDataTablePropertyArray(DataTableProcessor dataTableProcessor)
    {
        List<PropertyCollection> propertyCollections = new List<PropertyCollection>();
        for (int i = 0; i < dataTableProcessor.RawColumnCount; i++)
        {
            if (dataTableProcessor.IsCommentColumn(i))
            {
                // 注释列
                continue;
            }

            if (dataTableProcessor.IsIdColumn(i))
            {
                // 编号列
                continue;
            }

            string name = dataTableProcessor.GetName(i);
            if (!EndWithNumberRegex.IsMatch(name))
            {
                continue;
            }

            string propertyCollectionName = EndWithNumberRegex.Replace(name, string.Empty);
            int id = int.Parse(EndWithNumberRegex.Match(name).Value);

            PropertyCollection propertyCollection = null;
            foreach (PropertyCollection pc in propertyCollections)
            {
                if (pc.Name == propertyCollectionName)
                {
                    propertyCollection = pc;
                    break;
                }
            }

            if (propertyCollection == null)
            {
                propertyCollection =
                    new PropertyCollection(propertyCollectionName, dataTableProcessor.GetLanguageKeyword(i));
                propertyCollections.Add(propertyCollection);
            }

            propertyCollection.AddItem(id, name);
        }

        StringBuilder stringBuilder = new StringBuilder();
        bool firstProperty = true;
        foreach (PropertyCollection propertyCollection in propertyCollections)
        {
            if (firstProperty)
            {
                firstProperty = false;
            }
            else
            {
                stringBuilder.AppendLine().AppendLine();
            }

            stringBuilder
                .AppendFormat("\t\tprivate KeyValuePair<int, {1}>[] m_{0} = null;", propertyCollection.Name,
                    propertyCollection.LanguageKeyword).AppendLine()
                .AppendLine()
                .AppendFormat("\t\tpublic int {0}Count", propertyCollection.Name).AppendLine()
                .AppendLine("\t\t{")
                .AppendLine("\t\t\tget")
                .AppendLine("\t\t\t{")
                .AppendFormat("\t\t\t\treturn m_{0}.Length;", propertyCollection.Name).AppendLine()
                .AppendLine("\t\t\t}")
                .AppendLine("\t\t}")
                .AppendLine()
                .AppendFormat("\t\tpublic {1} Get{0}(int id)", propertyCollection.Name,
                    propertyCollection.LanguageKeyword).AppendLine()
                .AppendLine("\t\t{")
                .AppendFormat("\t\t\tforeach (KeyValuePair<int, {1}> i in m_{0})", propertyCollection.Name,
                    propertyCollection.LanguageKeyword).AppendLine()
                .AppendLine("\t\t\t{")
                .AppendLine("\t\t\t\tif (i.Key == id)")
                .AppendLine("\t\t\t\t{")
                .AppendLine("\t\t\t\t\treturn i.Value;")
                .AppendLine("\t\t\t\t}")
                .AppendLine("\t\t\t}")
                .AppendLine()
                .AppendFormat(
                    "\t\t\tthrow new GameFrameworkException(Utility.Text.Format(\"Get{0} with invalid id '{{0}}'.\", id));",
                    propertyCollection.Name).AppendLine()
                .AppendLine("\t\t}")
                .AppendLine()
                .AppendFormat("\t\tpublic {1} Get{0}At(int index)", propertyCollection.Name,
                    propertyCollection.LanguageKeyword).AppendLine()
                .AppendLine("\t\t{")
                .AppendFormat("\t\t\tif (index < 0 || index >= m_{0}.Length)", propertyCollection.Name)
                .AppendLine()
                .AppendLine("\t\t\t{")
                .AppendFormat(
                    "\t\t\t\tthrow new GameFrameworkException(Utility.Text.Format(\"Get{0}At with invalid index '{{0}}'.\", index));",
                    propertyCollection.Name).AppendLine()
                .AppendLine("\t\t\t}")
                .AppendLine()
                .AppendFormat("\t\t\treturn m_{0}[index].Value;", propertyCollection.Name).AppendLine()
                .Append("\t\t}");
        }

        if (propertyCollections.Count > 0)
        {
            stringBuilder.AppendLine().AppendLine();
        }

        stringBuilder
            .AppendLine("\t\tprivate void GeneratePropertyArray()")
            .AppendLine("\t\t{");

        firstProperty = true;
        foreach (PropertyCollection propertyCollection in propertyCollections)
        {
            if (firstProperty)
            {
                firstProperty = false;
            }
            else
            {
                stringBuilder.AppendLine().AppendLine();
            }

            stringBuilder
                .AppendFormat("\t\t\tm_{0} = new KeyValuePair<int, {1}>[]", propertyCollection.Name,
                    propertyCollection.LanguageKeyword).AppendLine()
                .AppendLine("\t\t\t{");

            int itemCount = propertyCollection.ItemCount;
            for (int i = 0; i < itemCount; i++)
            {
                KeyValuePair<int, string> item = propertyCollection.GetItem(i);
                stringBuilder.AppendFormat("\t\t\t\tnew KeyValuePair<int, {0}>({1}, {2}),",
                    propertyCollection.LanguageKeyword, item.Key.ToString(), item.Value).AppendLine();
            }

            stringBuilder.Append("\t\t\t};");
        }

        stringBuilder
            .AppendLine()
            .Append("\t\t}");

        return stringBuilder.ToString();
    }

        private sealed class PropertyCollection
        {
            private readonly string m_Name;
            private readonly string m_LanguageKeyword;
            private readonly List<KeyValuePair<int, string>> m_Items;

            public PropertyCollection(string name, string languageKeyword)
            {
                m_Name = name;
                m_LanguageKeyword = languageKeyword;
                m_Items = new List<KeyValuePair<int, string>>();
            }

            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            public string LanguageKeyword
            {
                get
                {
                    return m_LanguageKeyword;
                }
            }

            public int ItemCount
            {
                get
                {
                    return m_Items.Count;
                }
            }

            public KeyValuePair<int, string> GetItem(int index)
            {
                if (index < 0 || index >= m_Items.Count)
                {
                    throw new GameFrameworkException(Utility.Text.Format("GetItem with invalid index '{0}'.", index));
                }

                return m_Items[index];
            }

            public void AddItem(int id, string propertyName)
            {
                m_Items.Add(new KeyValuePair<int, string>(id, propertyName));
            }
        }
    }
}
