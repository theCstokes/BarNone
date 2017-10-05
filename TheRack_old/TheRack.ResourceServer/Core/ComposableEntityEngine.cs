using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheRack.DataTransfer.ComposableEntity;
using TheRack.Core.Extensions;
using TheRack.DataAccess;

namespace TheRack.ResourceServer.Core
{
    public class ComposableEntityEngine
    {
        public static Dictionary<string, object> Execute(ComposableEntityDTO composableEntity)
        {
            return composableEntity.Entities.Aggregate(new Dictionary<string, object>(),
                (result, request, idx) =>
                {
                    if (ComposableEntityRepositoryManager.EntityMap.ContainsKey(request.EntityType))
                    {
                        var repository = ComposableEntityRepositoryManager.EntityMap[request.EntityType]();
                        using (var dc = new DomainContext())
                        {
                            switch (request.RequestType)
                            {
                                case DataTransfer.Core.RequestType.Read:
                                    var dataList = repository.Get(dc);
                                    if (request.Key == null)
                                    {
                                        result[idx.ToString()] = dataList;
                                    }
                                    else
                                    {
                                        result[request.Key] = dataList;
                                    }
                                    break;
                            }
                        }
                    }
                    return result;
                });
        }
    }
}