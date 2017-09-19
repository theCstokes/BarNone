using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.DataAccess;
using TheRack.DataAccess.SQLBuilders;
using TheRack.DomainModel;
using static TheRack.DataAccessAdapter.DataAccessActions;

namespace TheRack.DataAccess.Engine
{
    public static partial class SQLEngine
    {
        public static List<TDomainModel> Execute<TDomainModel>(DomainContext context, 
            DataReadRequest<TDomainModel> request)
            where TDomainModel : BaseDomainModel<TDomainModel>, new()
        {
            var sql = GenerateRequest<TDomainModel>(GenerateReadRequest(request), request);
            var results = RunCommandAll(context, sql);
            return CreateEntities<TDomainModel>(request.TransformMap, results);
        }

        public static TDomainModel Execute<TDomainModel>(DomainContext context, 
            DataCreateRequest<TDomainModel> request)
            where TDomainModel : BaseDomainModel<TDomainModel>, new()
        {
            var sql = GenerateRequest<TDomainModel>(GenerateCreateRequest(request), request);
            var result = RunCommand(context, sql);
            return CreateEntity<TDomainModel>(request.TransformMap, result);
        }

        #region Private Static Member(s).
        //private delegate string RequestBuilder<TDomainModel>(IDataRequest<TDomainModel> request)
        //    where TDomainModel : BaseDomainModel<TDomainModel>, new();

        private static string GenerateRequest<TDomainModel>(string baseSQL,
            IDataRequest<TDomainModel> request)
            where TDomainModel : BaseDomainModel<TDomainModel>, new()
        {
            var sb = new StringBuilder(baseSQL);
            
            // TODO - Better where.
            if (request.WhereClauses != null && request.Type == RequestType.LOAD)
            {
                sb.Append($" {GenerateWhereRequest<TDomainModel>(request)}");
            }
            if (request.Type == RequestType.UPDATE)
            {
                sb.Append($" {GenerateReturnRequest<TDomainModel>(request)}");
            }

            return sb.ToString();
        }
        private static SQLResults RunCommand(DomainContext context, string sql)
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

        private static List<SQLResults> RunCommandAll(DomainContext context, string sql)
        {
            using (var cmd = new NpgsqlCommand(sql, context.Connection, context.Transaction))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var listResults = new List<SQLResults>();
                    while (reader.Read())
                    {
                        var result = new SQLResults(Enumerable.Range(0, reader.FieldCount)
                            .ToDictionary(reader.GetName, reader.GetValue));
                        listResults.Add(result);
                    }
                    return listResults;
                }
            }
        }

        private static TDomainModel CreateEntity<TDomainModel>(
            Dictionary<string, WriteAction<TDomainModel>> transformMap, SQLResults result)
            where TDomainModel : BaseDomainModel<TDomainModel>, new()
        {
            var model = new TDomainModel();

            foreach (var pair in result)
            {
                if (!transformMap.ContainsKey(pair.Key)) continue;
                transformMap[pair.Key](model, pair.Value);
            }
            return model;
        }

        private static List<TDomainModel> CreateEntities<TDomainModel>(
            Dictionary<string, WriteAction<TDomainModel>> transformMap, List<SQLResults> resultList)
            where TDomainModel : BaseDomainModel<TDomainModel>, new()
        {
            return resultList.Select(result =>
            {
                var model = new TDomainModel();

                foreach (var pair in result)
                {
                    if (!transformMap.ContainsKey(pair.Key)) continue;
                    transformMap[pair.Key](model, pair.Value);
                }
                return model;
            }).ToList();
        }
        #endregion

    }
}
