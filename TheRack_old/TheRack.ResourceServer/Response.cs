using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace TheRack.ResourceServer
{
    public class Response : IHttpActionResult
    {
        private HttpStatusCode status;
        private object source;

        public Response(HttpStatusCode status, object source)
        {
            this.status = status;
            this.source = source;

            var payloadData = Json.Stringify(source);

            Payload = new HttpResponseMessage(status)
            {
                Content = new StringContent(payloadData)
            };
        }

        public Response(HttpStatusCode status)
        {
            this.status = status;

            Payload = new HttpResponseMessage(status)
            {
            };
        }

        public HttpResponseMessage Payload { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Payload);
        }
    }
}