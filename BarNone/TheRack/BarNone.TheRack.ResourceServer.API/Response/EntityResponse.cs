﻿using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DomainModel;
using BarNone.Shared.DomainModel.Core;
using BarNone.TheRack.DomainModel;
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
        public static IActionResult Entity<TDomainModel, TDTO>(TDomainModel entity, HttpStatusCode code = HttpStatusCode.OK)
            where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, new()
            where TDTO : BaseDTO<TDTO>, new()
        {
            var response = new EntityDTO
            {
                Entity = entity.BuildDTO()
            };

            return CreateResult(response, code);
        }

        public static IActionResult EntityDetail<TDomainModel, TDTO, TDetailDTO>(TDomainModel entity, 
            HttpStatusCode code = HttpStatusCode.OK)
            where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, IDetailDomainModel<TDTO, TDetailDTO>, new()
            where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
            where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
        {
            var dto = entity.BuildDTO();
            dto.Details = entity.BuildDetailDTO();
            var response = new EntityDTO
            {
                Entity = dto
            };

            return CreateResult(response, code);
        }

        //public static IActionResult Entity(IDomainModel entity, HttpStatusCode code = HttpStatusCode.OK)
        //{
        //    var response = new EntityDTO
        //    {
        //        Entity = entity
        //    };

        //    return CreateResult(response, code);
        //}
        public static IActionResult EntityDTO<TDTO>(TDTO entity, HttpStatusCode code = HttpStatusCode.OK)
            where TDTO : BaseDTO<TDTO>, new()
        {

            var response = new EntityDTO
            {
                Entity = entity
            };

            return CreateResult(response, code);
        }


        public static IActionResult Enumerable(IEnumerable<IDomainModel> entities, HttpStatusCode code = HttpStatusCode.OK)
        {

            var response = new EnumerableDTO
            {
                Count = entities.Count(),
                Entities = entities
            };

            return CreateResult(response, code);
        }

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
            //return new ObjectResult(JsonConvert.SerializeObject(source,
            //    new JsonSerializerSettings
            //    {
            //        ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //        NullValueHandling = NullValueHandling.Ignore
            //    }))
            //{
            //    StatusCode = (int)code
            //};

            return new ObjectResult(source)
            {
                StatusCode = (int)code
            };
        }
    }
}