using BarNone.Shared.DomainModel.Core;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository;
using BarNone.TheRack.Repository.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRack.ResourceServer.API.Response;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Flex
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class FlexController
    {
        [HttpPost]
        public IActionResult Flex([FromBody] FlexRequestEntity value)
        {
            var context = new DomainContext();
            var flexResponse = value.Requests.Aggregate(new FlexResponse(), (result, entity) =>
            {
                var name = entity.Name ?? entity.Type;

                result.Results[name] = _process(context, entity);

                return result;
            });

            return EntityResponse.Entity(flexResponse, System.Net.HttpStatusCode.OK);
        }

        private dynamic _process(DomainContext context, FlexEntity entity)
        {
            var repo = FlexMap.Map[entity.Type](context);
            var name = entity.Name ?? entity.Type;

            var result = new FlexResponseEntity();

            switch (entity.RequestType)
            {
                case FlexRequestType.Get:
                    var elements = repo.Get();
                    result.Result = elements.Select(e =>
                    {
                        var re = new FlexResponseEntity();
                        re.Result = e;

                        if (entity.Details != null)
                        {
                            re.Details = entity.Details.Select(detail => _process(context, detail)).ToList();
                        }
                        return re;
                    });
                    break;
                case FlexRequestType.Create:
                    result = repo.Create(entity.Entity);
                    break;
            }

            return result;
        }
    }

    public class FlexResponse
    {

        public FlexResponse()
        {
            Results = new Dictionary<string, FlexResponseEntity>();
        }

        public Dictionary<string, FlexResponseEntity> Results { get; set; }
    }

    public class FlexResponseEntity
    {
        public dynamic Result { get; set; }

        public List<dynamic> Details { get; set; }

    }

    public class FlexRequestEntity
    {
        public List<FlexEntity> Requests { get; set; }
    }

    public class FlexEntity
    {
        public string Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FlexRequestType RequestType { get; set; }

        public string Name { get; set; }

        public dynamic Entity { get; set; }

        public List<FlexEntity> Details { get; set; }
    }

    public enum FlexRequestType
    {
        Get = 0,
        Create = 1
    }

    public static class FlexEntityType
    {
        public static string LIFT = "LIFT";

        public static string LIFT_FOLDER = "LIFT_FOLDER";

        public static string USER = "USER";

        public static string BODY_DATA = "BODY_DATA";

        public static string BODY_DATA_FRAME = "BODY_DATA_FRAME";
    }

    public class FlexMap
    {

        public delegate IRepository IRepositoryBuilder(DomainContext context);

        public static Dictionary<string, IRepositoryBuilder> Map { get; } =
            new Dictionary<string, IRepositoryBuilder>
            {
                [FlexEntityType.LIFT] = (c) => new LiftRepository(c),
                [FlexEntityType.LIFT_FOLDER] = (c) => new LiftFolderRepository(c),
                [FlexEntityType.USER] = (c) => new UserRepository(c),
                [FlexEntityType.BODY_DATA] = (c) => new BodyDataRepository(c),
                [FlexEntityType.BODY_DATA_FRAME] = (c) => new BodyDataFrameRepository(c)
            };
    }
}
