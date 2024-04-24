using System;
using System.IO;

namespace DataTableGenerator
{
    public sealed partial class DataTableProcessor
    {
        public abstract class DataProcessor
        {
            public abstract Type Type { get; }

            public abstract bool IsId { get; }

            public abstract bool IsComment { get; }

            public abstract bool IsSystem { get; }

            public abstract string LanguageKeyword { get; }

            /// <summary>
            /// 对应 Excel 中的数据类型，至于为什么是数组
            /// 因为可以匹配类型别名的情况
            /// 比如 string & System.String 都对应了同一种数据类型
            /// </summary>
            public abstract string[] GetTypeStrings();

            public abstract void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter,
                string value);
        }
    }
}