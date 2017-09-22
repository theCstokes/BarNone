using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class SQLGeneratorRequest
    {
        public SQLGeneratorRequest(string schemaName, string tableName, Dictionary<string, string> dataMap)
        {
            SchemaName = schemaName;
            TableName = tableName;
            DataMap = dataMap;
        }

        public string SchemaName { get; private set; }
        public string TableName { get; private set; }
        public Dictionary<string, string> DataMap { get; private set; }
    }
}
