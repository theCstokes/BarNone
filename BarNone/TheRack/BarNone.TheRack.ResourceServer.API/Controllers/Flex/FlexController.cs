using BarNone.Shared.DomainModel.Core;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository;
using BarNone.TheRack.Repository.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var result = value.Requests.Select(s =>
            {
                var repo = FlexMap.Map[s.Type](context);
                return repo.Get();
            });

            return EntityResponse.Enumerable(result, System.Net.HttpStatusCode.OK);
            //var repo = FlexMap.Map[value.]
            //return null;
        }
    }

    public class FlexResponse
    {
        public string Type { get; set; }
        public dynamic Result { get; set; }
    }

    public class FlexRequestEntity
    {
        public List<FlexEntity> Requests { get; set; }
    }

    public class FlexEntity
    {
        public string Type { get; set; }

        public FlexRequestType Request { get; set; }

        public dynamic Entity { get; set; }
    }

    public enum FlexRequestType
    {
        GET = 0,
        CREATE = 1
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
