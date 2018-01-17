using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DomainModel;
using BarNone.Shared.DomainModel.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.ResourceServer.API.Response
{
    struct EntityDTO
    {
        public object Entity { get; set; }
    }

    struct EnumerableDTO
    {
        public int Count { get; set; }

        public IEnumerable<object> Entities { get; set; }
    }

    struct ErrorDTO
    {
        public string Message { get; set; }
    }

    public class EntityResponse : IResponse
    {
        //public static IActionResult Entity<TDomainModel, TDTO>(TDomainModel entity, HttpStatusCode code = HttpStatusCode.OK)
        //    where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, new()
        //    where TDTO : BaseDTO<TDTO>, new()
        //{
        //    var response = new EntityDTO
        //    {
        //        Entity = entity.BuildDTO()
        //    };

        //    return CreateResult(response, code);
        //}

        //public static IActionResult EntityDetail<TDomainModel, TDTO, TDetailDTO>(TDomainModel entity, 
        //    HttpStatusCode code = HttpStatusCode.OK)
        //    where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, IDetailDomainModel<TDTO, TDetailDTO>, new()
        //    where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        //    where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
        //{
        //    var dto = entity.BuildDTO();
        //    dto.Details = entity.BuildDetailDTO();
        //    var response = new EntityDTO
        //    {
        //        Entity = dto
        //    };

        //    return CreateResult(response, code);
        //}

        public static IActionResult Entity(object entity, HttpStatusCode code = HttpStatusCode.OK)
        {
            //var response = new EntityDTO
            //{
            //    Entity = entity
            //};

            return CreateResult(entity, code);
        }

        public static IActionResult Response(IDomainModel entity, HttpStatusCode code = HttpStatusCode.OK)
        {
            //if (config == null) config = new ConvertConfig(1);

            var dto = Converters.NewConvertion().GetConverterFromData(entity.GetType()).CreateDTO(entity);
            var response = new EntityDTO
            {
                Entity = dto
            };

            return CreateResult(response, code);
        }

        public static IActionResult DetailResponse(IDomainModel entity, HttpStatusCode code = HttpStatusCode.OK)
        {
            //if (config == null) config = new ConvertConfig(2);

            //var dto = entity.CreateDTO(config);

            var dto = Converters.NewConvertion().GetConverterFromData(entity.GetType()).CreateDTO(entity);
            var response = new EntityDTO
            {
                Entity = dto
            };

            return CreateResult(response, code);
        }

        public static IActionResult Response(IEnumerable<IDomainModel> entities, HttpStatusCode code = HttpStatusCode.OK)
        {
            var response = new EnumerableDTO
            {
                Count = entities.Count(),
                Entities = entities
                .Select(entity => Converters.NewConvertion().GetConverterFromData(entity.GetType()).CreateDTO(entity)).ToList()
            };

            return CreateResult(response, code);
        }

        //public static IActionResult Enumerable(IEnumerable<IDTOTransformable> entities, HttpStatusCode code = HttpStatusCode.OK)
        //{

        //    var response = new EnumerableDTO
        //    {
        //        Count = entities.Count(),
        //        Entities = entities
        //    };

        //    return CreateResult(response, code);
        //}

        //public static IActionResult Enumerable(List<IDTOTransformable> entities, HttpStatusCode code = HttpStatusCode.OK)
        //{

        //    var response = new EnumerableDTO
        //    {
        //        Count = entities.Count(),
        //        Entities = entities
        //    };

        //    return CreateResult(response, code);
        //}

        //public static IActionResult Enumerable(dynamic result, HttpStatusCode code = HttpStatusCode.OK)
        //{

        //    //var response = new EnumerableDTO
        //    //{
        //    //    Count = entities.Count(),
        //    //    Entities = entities
        //    //};

        //    return CreateResult(result, code);
        //}

        public static IActionResult Error(Exception e, HttpStatusCode code = HttpStatusCode.InternalServerError)
        {

            var response = new ErrorDTO
            {
                Message = e.Message
            };

            return CreateResult(response, code);
        }

        private static IActionResult CreateResult(object source, HttpStatusCode code)
        {
            return new ObjectResult(source)
            {
                StatusCode = (int)code
            };
        }
    }
}
