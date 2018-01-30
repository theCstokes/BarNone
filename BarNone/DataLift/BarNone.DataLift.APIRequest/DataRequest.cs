using BarNone.Shared.DataTransfer.Response;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.APIRequest
{
    /// <summary>
    /// Data Post and Get request abstraction
    /// </summary>
    public class DataRequest
    {
        #region Public Static Member(s).
        /// <summary>
        /// Get data from the URL
        /// </summary>
        /// <typeparam name="T">Data type fetched</typeparam>
        /// <param name="url">Location being fetched from</param>
        /// <returns>The object requested</returns>
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

        /// <summary>
        /// Post erialized data to <paramref name="url"/>
        /// </summary>
        /// <typeparam name="T">Data type posted</typeparam>
        /// <param name="url">Location being posted to</param>
        /// <param name="data">Data being posted to <paramref name="data"/></param>
        /// <returns>Response of post deserialized</returns>
        public static async Task<T> Post<T>(string url, object data)
            where T : class
        {
            using (var client = CreateClient())
            {
                using (var content =
                    new StringContent(JsonConvert.SerializeObject(data, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }), Encoding.UTF8, "application/json"))
                {
                    //File.WriteAllText(@"C:\Users\Aamir\Documents\McMaster\Year_4\Capstone\File1.json", JsonConvert.SerializeObject(data));

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    var result = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
        }

        /// <summary>
        /// Post data to <paramref name="url"/>
        /// </summary>
        /// <param name="url">Location being posted to</param>
        /// <param name="data">Data being posted to <paramref name="data"/></param>
        /// <returns>Void Task</returns>
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
        /// <summary>
        /// Response error handled by the Rack
        /// </summary>
        public ResponseErrorDTO Error { get; private set; }

        #endregion

        #region Private Member(s).
        /// <summary>
        /// Opens an Http Client for an authorized user
        /// </summary>
        /// <returns>User HttpCient to retrieve or post data to</returns>
        private static HttpClient CreateClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenManager.Token}");
            return httpClient;
        }

        #endregion

    }
}
