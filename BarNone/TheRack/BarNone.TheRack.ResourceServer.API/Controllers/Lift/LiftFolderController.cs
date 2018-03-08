using BarNone.Shared.Core;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRack.ResourceServer.API.Response;

namespace BarNone.TheRack.ResourceServer.API.Controllers
{
    /// <summary>
    /// LiftFolder endpoint controller.
    /// </summary>
    /// <seealso cref="BarNone.TheRack.ResourceServer.API.Controllers.Core.DefaultDetailController{BarNone.Shared.DataTransfer.LiftFolderDTO, BarNone.Shared.DomainModel.LiftFolder, BarNone.TheRack.Repository.LiftFolderRepository}" />
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class LiftFolderController : DefaultDetailController<LiftFolderDTO, LiftFolder, LiftFolderRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftFolderController"/> class.
        /// </summary>
        public LiftFolderController() : base((context) => new LiftFolderRepository(context))
        {

        }

        public override IActionResult GetWithDetailsByID(int id)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = new LiftFolderRepository(context))
                {
                    var result = repo.GetWithDetails(id);

                    var dto = Converters.NewConvertion()
                        .GetConverterFromData(result.GetType()).CreateDTO(result) as LiftFolderDTO;

                    var f = ObjectUtils.Clone(dto);

                    f.Details.Lifts.ForEach(l =>
                    {
                        var p = l.Details.Parent.Clone();
                        p.Details = null;
                        l.Details.Parent = p;
                    });

                    var response = new EntityDTO
                    {
                        Entity = f
                    };

                    var s = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        //ObjectCreationHandling = ObjectCreationHandling.Replace,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        //PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                    //return new ObjectResult(response)
                    //{
                    //    StatusCode = (int) 200
                    //};

                    //return CreateResult(response, code);

                    //result.
                    return EntityResponse.DetailResponse(result);
                }
            }
        }
    }
}
