using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Flex;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.FlexEngine;
using BarNone.TheRack.Repository;
using BarNone.TheRack.Repository.Core;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using BarNone.TheRack.ResourceServer.API.Response;
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
    /// <summary>
    /// 
    /// </summary>
    struct FlexEntity
    {
        /// <summary>
        /// The builder for the repository.
        /// </summary>
        public RepositoryBuilder Builder;

        /// <summary>
        /// The type of entity managed by the repository.
        /// </summary>
        public Type Type;
    }

    /// <summary>
    /// Endpoint controller for managing flex requests of very large size.
    /// </summary>
    /// <seealso cref="BarNone.TheRack.ResourceServer.API.Controllers.Core.BaseController" />
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class FlexController : BaseController
    {
        /// <summary>
        /// Delegate for building repository.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Route to make flex request.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Flex()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                HttpContext.Request.Body.CopyTo(ms);
                byte[] data = ms.ToArray();
                var jsonString = Encoding.ASCII.GetString(data);
                //var dto = JsonConvert.DeserializeObject<LiftDTO>(jsonString);
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new JsonPropertiesResolver()
                };
                var dto = JsonConvert.DeserializeObject<FlexDTO>(jsonString,settings);

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

                            var val = ((JObject)entity.Entity).ToObject(flex.Type, JsonSerializer.Create(settings));
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
    }
}
