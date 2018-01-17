using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Flex;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.FlexEngine;
using BarNone.TheRack.Repository;
using BarNone.TheRack.Repository.Core;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.ResourceServer.API.Response;
using static BarNone.TheRack.ResourceServer.API.Controllers.Flex.FlexController;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Flex
{
    struct FlexEntity
    {
        public RepositoryBuilder Builder;
        public Type Type;
    }

    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class FlexController : BaseController
    {
        public delegate IRepository RepositoryBuilder(DomainContext context);

        private Dictionary<string, FlexEntity> repoMap = new Dictionary<string, FlexEntity>
        {
            {
                FlexEntityType.LIFT,
                new FlexEntity
                {
                    Type = typeof (LiftDTO),
                    Builder = (dc) => new LiftRepository(dc)
                }
            }
        };

        [HttpPost]
        public IActionResult Flex()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                HttpContext.Request.Body.CopyTo(ms);
                byte[] data = ms.ToArray();
                var jsonString = Encoding.ASCII.GetString(data);
                //var dto = JsonConvert.DeserializeObject<LiftDTO>(jsonString);

                var dto = JsonConvert.DeserializeObject<FlexDTO>(jsonString);

                try
                {
                    using (var context = new DomainContext(UserID))
                    {
                        var entities = dto.Entities.Select(entity =>
                        {
                            if (!repoMap.ContainsKey(entity.Resource)) return null;

                            var entityResult = new FlexEntityDTO
                            {
                                Resource = entity.Resource
                            };

                            var flex = repoMap[entity.Resource];
                            var repo = flex.Builder(context);

                            var val = ((JObject)entity.Entity).ToObject(flex.Type);
                            entityResult.Entity = repo.Create(val);

                            return entityResult;
                        }).ToList();

                        context.SaveChanges();
                        return EntityResponse.Entity(new FlexDTO
                        {
                            Entities = entities
                        });
                    }
                }
                catch (Exception e)
                {
                    return EntityResponse.Error(e);
                }
            }
        }

        //private T ConvertObject<T>(Object obj)
        //{
        //    return ((JObject)obj).ToObject<>();
        //}
    }

    //public static class FlexResourceType
    //{
    //    public static string Lift = "Lift";
    //}
}
