using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarNone.TheRack.ResourceServer.API.Controllers
{
    class RequestPipelineData
    {

    }

    delegate RequestPipelineData IRequestPipeExecutable(RequestPipelineData data);

    interface IRequestPipe
    {
        RequestPipelineData Execute(RequestPipelineData data);
    }

    class RequestPipline
    {
        public static RequestPipline Create()
        {
            return new RequestPipline();
        }

        public RequestPipline AddPipe(IRequestPipe pipe)
        {
            return this;
        }

        public RequestPipline ThenPipe(IRequestPipeExecutable pipe)
        {
            return this;
        }
    }

    class DemoRepository
    {
        public RequestPipelineData GetAll(RequestPipelineData data)
        {
            return data;
        }
    }

    public class DemoController
    {

        public void Get(Object data)
        {
            var pipeline = RequestPipline.Create();
        }
    }
}
