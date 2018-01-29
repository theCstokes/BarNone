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
using BarNone.Shared.Core;
using Newtonsoft.Json.Linq;

namespace TheRack.ResourceServer.API.Response
{
    /// <summary>
    /// Standard result dto wrapper.
    /// </summary>
    struct EntityDTO
    {
        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public object Entity { get; set; }
    }

    /// <summary>
    /// Enumerable result dto wrapper.
    /// </summary>
    struct EnumerableDTO
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IEnumerable<object> Entities { get; set; }
    }

    /// <summary>
    /// Error result dto wrapper.
    /// </summary>
    struct ErrorDTO
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
    }

    /// <summary>
    /// Response helper to create API responses.
    /// </summary>
    public static class EntityResponse
    {
        /// <summary>
        /// Create the API response for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="code">The status code.</param>
        /// <returns></returns>
        public static IActionResult Entity(object entity, HttpStatusCode code = HttpStatusCode.OK)
        {
            return CreateResult(entity, code);
        }

        /// <summary>
        /// Creates the API responses for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="code">The status code.</param>
        /// <returns></returns>
        public static IActionResult Response(IDomainModel entity, HttpStatusCode code = HttpStatusCode.OK)
        {
            var dto = Converters.NewConvertion().GetConverterFromData(entity.GetType()).CreateDTO(entity);
            var response = new EntityDTO
            {
                Entity = dto
            };

            return CreateResult(response, code);
        }

        /// <summary>
        /// Creates the detailed API the response for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="code">The status code.</param>
        /// <returns></returns>
        public static IActionResult DetailResponse(IDomainModel entity, HttpStatusCode code = HttpStatusCode.OK)
        {
            var dto = Converters.NewConvertion()
                .GetConverterFromData(entity.GetType()).CreateDTO(entity) as IParentDTO;
            var response = new EntityDTO
            {
                Entity = dto
            };

            return CreateResult(response, code);
        }

        private static ITrackableDTO FilterDetail(IParentDTO dto)
        {
            _FilterDetail(dto, 0, 3);
            return dto;
        }

        private static IParentDTO _FilterDetail(IParentDTO dto, int depth, int target)
        {
            if (depth == target) return null;
            //var d = ((IEnumerable<KeyValuePair<string, JToken>>)dto.Details);

            //  Dictionary<string, string> newDict =
            //((IEnumerable<KeyValuePair<string, JToken>>)dto.Details)
            //           .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

            var d = (IDictionary<string, object>)dto.Details;

            //var t = d.Select(pair => pair.Value.Type);
            //.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.);

            dto.Details = _FilterDetail(dto.Details, depth + 1, target);
            return dto;
        }


        /// <summary>
        /// Creates the API responses for the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="code">The status code.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates the errors API response the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static IActionResult Error(Exception e, HttpStatusCode code = HttpStatusCode.InternalServerError)
        {

            var response = new ErrorDTO
            {
                Message = e.Message
            };

            return CreateResult(response, code);
        }

        /// <summary>
        /// Creates the result.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        private static IActionResult CreateResult(object source, HttpStatusCode code)
        {
            return new ObjectResult(source)
            {
                StatusCode = (int)code
            };
        }
    }
}
