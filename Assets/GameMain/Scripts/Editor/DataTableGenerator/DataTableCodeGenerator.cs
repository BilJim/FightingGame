using System.Text;

namespace DataTableGenerator
{
    public delegate void DataTableCodeGenerator(DataTableProcessor dataTableProcessor, StringBuilder codeContent,
        object userData);
}