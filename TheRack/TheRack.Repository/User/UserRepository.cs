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

namespace TheRack.Repository
{
    public class UserRepository : BaseRepository<UserDTO, User>
    {

        public User Get(Expression<Func<User, bool>> where = null)
        {
            var dc = new DomainContext();
            var adapter = new UserAdapter();
            var dataMap = adapter.CreateMapForGet();
            var request = new DataRequest
            {
                Type = RequestType.LOAD,
                MetaData = new EntityMetaData
                {
                    SchemaName = "public",
                    TableName = "User"
                },
                Entity = new DataEntity(dataMap),
            };

            request.WhereClauses = WhereClauseBuilder<User>.Execute(where, request);
            var sql = SQLGenerator.Execute(request);
            var result = SQLProcessor.Execute(dc, sql);

            var user = new User();

            foreach (var pair in result)
            {
                adapter.WriteMap[pair.Key](user, pair.Value);
            }
            
            dc.Commit();
            return user;
        }

        public User Create(UserDTO dto)
        {
            var dc = new DomainContext();
            var adapter = new UserAdapter();
            var dataMap = adapter.CreateReadMap(dto);
            var request = new DataRequest
            {
                Type = RequestType.CREATE,
                MetaData = new EntityMetaData
                {
                    SchemaName = "public",
                    TableName = "User"
                },
                Entity = new DataEntity(dataMap)
            };
            var sql = SQLGenerator.Execute(request);
            var result = SQLProcessor.Execute(dc, sql);
            var user = adapter.ToDomainModel(dto);
            user.ID = Convert.ToInt32(result["ID"]);
            dc.Commit();
            return user;
        }

        public User Update(UserDTO dto)
        {
            var dc = new DomainContext();
            var adapter = new UserAdapter();
            var dataMap = adapter.CreateReadMap(dto);
            var request = new DataRequest
            {
                Type = RequestType.UPDATE,
                MetaData = new EntityMetaData
                {
                    SchemaName = "public",
                    TableName = "User"
                },
                Entity = new DataEntity(dto.ID, dataMap),
                UpdateFilter = dto.UpdateFilter.Select(filter =>
                {
                    return new UpdateRequest
                    {
                        Name = filter.Name,
                        Operation = WhereOperation.GetOperation(filter.Operation)
                    };
                }).ToList()
            };
            var sql = SQLGenerator.Execute(request);
            var result = SQLProcessor.Execute(dc, sql);
            var user = adapter.ToDomainModel(dto);
            //user.ID = Convert.ToInt32(result["ID"]);
            dc.Commit();
            return user;
        }
    }
}
