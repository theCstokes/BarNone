using BarNone.Shared.DataTransfer.Auth;
using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DataTransfer.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.APIRequest
{
    public class DataRequest
    {
        #region Private Field(s).
        private readonly string _url;
        private readonly string _httpMethod;
        #endregion

        #region Private Constructor(s).
        private DataRequest(string url, string httpMethod)
        {
            _url = url;
            _httpMethod = httpMethod;
        }
        #endregion

        #region Public Static Member(s).
        public static async Task<T> Get<T>(string url)
            where T : class
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public static async Task<T> Post<T>(string url, object data)
            where T : class
        {
            using (var client = CreateClient())
            {
                using (var content =
                    new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
                {
                    File.WriteAllText(@"C:\Users\Aamir\Documents\McMaster\Year_4\Capstone\File1.json", JsonConvert.SerializeObject(data));

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    var result = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
        }

        public static async Task Post(string url, byte[] data)
        {
            WebRequest request = WebRequest.Create(url);
            request.Headers.Add("Authorization", $"Bearer {TokenManager.Token}");
            request.Method = "POST";
            request.ContentLength = data.Length;

            var dataStream = request.GetRequestStream();
            await dataStream.WriteAsync(data, 0, data.Length);
            dataStream.Close();
        }
        #endregion

        #region Public Property(s).
        public ResponseErrorDTO Error { get; private set; }
        #endregion

        #region Public Member(s).
        #endregion

        #region Private Member(s).
        private static HttpClient CreateClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenManager.Token}");
            return httpClient;
        }

        //private HttpWebRequest CreateRequest()
        //{
        //    Uri address = new Uri(_url);

        //    HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

        //    request.Method = _httpMethod;
        //    request.Timeout = 600000;

        //    request.ContentType = "application/json";
        //    request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {TokenManager.Token}");

        //    return request;
        //}
        #endregion
    }
}
