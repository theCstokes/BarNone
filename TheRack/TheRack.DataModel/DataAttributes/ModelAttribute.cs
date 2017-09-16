using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataModel
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelAttribute : Attribute
    {
        public ModelAttribute(string schemaName, string tableName)
        {
            SchemaName = schemaName;
            TableName = tableName;
        }

        public string SchemaName { get; private set; }
        public string TableName { get; private set; }
    }
}
