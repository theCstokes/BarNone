//using Microsoft.AspNetCore.Mvc.Formatters;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BarNone.TheRack.ResourceServer.API.Controllers.Lift
//{
//    public class VideoOutputFormatter
//    {
//        public bool CanWriteResult(OutputFormatterCanWriteContext context)
//        {
//            if (context == null)
//                throw new ArgumentNullException(nameof(context));

//            if (context.Object is PushStreamContent)
//                return true;

//            return false;
//        }

//        public async Task WriteAsync(OutputFormatterWriteContext context)
//        {
//            if (context == null)
//                throw new ArgumentNullException(nameof(context));

//            using (var stream = ((PushStreamContent)context.Object))
//            {
//                var response = context.HttpContext.Response;
//                if (context.ContentType != null)
//                {
//                    response.ContentType = context.ContentType.ToString();
//                }

//                await stream.CopyToAsync(response.Body);
//            }
//        }
//    }
//}
