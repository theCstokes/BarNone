using TheRack.DataAccess;
using TheRack.DataAccessAdapter;
using TheRack.DataTransfer.UserDTO;
using TheRack.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheRack.DataAccess.Engine;

namespace TheRack.Repository
{
    public class UserRepository : BaseRepository<UserDTO, User>
    {

        public List<User> Get(Expression<Func<User, bool>> where = null)
        {
            var dc = new DomainContext();
            var adapter = new UserAdapter();
            var dataMap = adapter.CreateMapForGet();
            var request = new DataReadRequest<User>
            {
                Type = RequestType.LOAD,
                MetaData = new EntityMetaData
                {
                    SchemaName = adapter.SchemaName,
                    TableName = adapter.TableName
                },
                Properties = adapter.Properties,
                TransformMap = adapter.FullWriteMap
            };

            if (where != null)
            {
                request.WhereClauses = WhereClauseBuilder<User>.Execute(where);
            }

            var userList = SQLEngine.Execute<User>(dc, request);

            dc.Commit();

            return userList;
        }

        //public List<User> Get(Expression<Func<User, bool>> where = null)
        //{
        //    var dc = new DomainContext();
        //    var adapter = new UserAdapter();
        //    var dataMap = adapter.CreateMapForGet();
        //    var request = new DataRequest
        //    {
        //        Type = RequestType.LOAD,
        //        MetaData = new EntityMetaData
        //        {
        //            SchemaName = "public",
        //            TableName = "User"
        //        },
        //        Entity = new DataEntity(dataMap),
        //    };

        //    if (where != null)
        //    {
        //        request.WhereClauses = WhereClauseBuilder<User>.Execute(where, request);
        //    }
            
        //    var sql = SQLGenerator.Execute(request);
        //    var resultList = SQLProcessor.ExecuteAll(dc, sql);

        //    dc.Commit();

        //    return CreateEntities(adapter, resultList);
        //}

        //public User Create(UserDTO dto)
        //{
        //    var dc = new DomainContext();
        //    var adapter = new UserAdapter();
        //    var dataMap = adapter.CreateReadMap(dto);
        //    var request = new DataRequest
        //    {
        //        Type = RequestType.CREATE,
        //        MetaData = new EntityMetaData
        //        {
        //            SchemaName = "public",
        //            TableName = "User"
        //        },
        //        Entity = new DataEntity(dataMap)
        //    };
        //    var sql = SQLGenerator.Execute(request);
        //    var result = SQLProcessor.Execute(dc, sql);
        //    var user = adapter.ToDomainModel(dto);
        //    user.ID = Convert.ToInt32(result["ID"]);
        //    dc.Commit();
        //    return user;
        //}

        //public User Update(UserDTO dto)
        //{
        //    var dc = new DomainContext();
        //    var adapter = new UserAdapter();
        //    var dataMap = adapter.CreateReadMap(dto);
        //    var request = new DataRequest
        //    {
        //        Type = RequestType.UPDATE,
        //        MetaData = new EntityMetaData
        //        {
        //            SchemaName = "public",
        //            TableName = "User"
        //        },
        //        Entity = new DataEntity(dto.ID, dataMap),
        //        UpdateFilter = dto.UpdateFilter.Select(filter =>
        //        {
        //            return new UpdateRequest
        //            {
        //                Name = filter.Name,
        //                Operation = WhereOperation.GetOperation(filter.Operation)
        //            };
        //        }).ToList()
        //    };
        //    var sql = SQLGenerator.Execute(request);
        //    var result = SQLProcessor.Execute(dc, sql);
        //    var user = adapter.ToDomainModel(dto);
        //    //user.ID = Convert.ToInt32(result["ID"]);
        //    dc.Commit();
        //    return user;
        //}
    }
}
