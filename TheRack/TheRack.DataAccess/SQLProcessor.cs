using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class SQLProcessor
    {
        public static SQLResults Execute(DomainContext context, string sql)
        {
            using (var cmd = new NpgsqlCommand(sql, context.Connection, context.Transaction))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = new SQLResults(Enumerable.Range(0, reader.FieldCount)
                            .ToDictionary(reader.GetName, reader.GetValue));
                        return result;
                    }
                }
            }
            return null;
        }
    }
}
