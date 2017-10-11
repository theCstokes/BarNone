using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.ResourceServer.API.Response
{
    public class EnumerableResponse : IResponse
    {
        public int Count { get; set; }
        public IEnumerable<object> Entities { get; set; }

        //public static IActionResult Create(IEnumerable<object> entities, HttpStatusCode code = HttpStatusCode.OK)
        //{
        //    //var msg = new HttpResponseMessage(code);

        //    var response = new EnumerableResponse
        //    {
        //        Count = entities.Count(),
        //        Entities = entities
        //    };

        //    //msg.Content = new StringContent(JsonConvert.SerializeObject(response), Encoding.UTF8, "application/json");

        //    //return msg;

        //    var or = new ObjectResult(response);
        //    or.StatusCode = (int)code;

        //    return or;
        //}
    }
}
