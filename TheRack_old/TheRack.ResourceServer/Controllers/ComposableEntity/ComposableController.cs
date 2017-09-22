using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using TheRack.DataAccess;
using TheRack.DataTransfer.ComposableEntity;
using TheRack.DataTransfer.Core;
using TheRack.Repository;
using TheRack.Core.Extensions;
using TheRack.ResourceServer.Core;

namespace TheRack.ResourceServer.Controllers.ComposableEntity
{
    [Authorize]
    [RoutePrefix("api/v1")]
    public class ComposableController : ApiController
    {
        private delegate IRepository RepositoryBuilder();
        private Dictionary<string, RepositoryBuilder> _entityMap = new Dictionary<string, RepositoryBuilder>
        {
            [EntityTypes.USER] = () => new UserRepository()
        };

        [HttpGet]
        [Route("ComposableEntity/Types")]
        public IHttpActionResult Get()
        {
            return new Response(HttpStatusCode.OK, _entityMap.Keys);
        }

        [HttpPatch]
        [Route("ComposableEntity")]
        public IHttpActionResult Patch([FromBody] ComposableEntityDTO composableEntity)
        {
            var responseResult = ComposableEntityEngine.Execute(composableEntity);
            return new Response(HttpStatusCode.OK, responseResult);
        }
    }

    class EntityTypes
    {
        public const string USER = "User";
    }
}