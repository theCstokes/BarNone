using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.APIRequest
{
    public class TokenManager
    {
        public static async Task<AuthDTO> Authorize(string userName, string password)
        {

            var postData = new Dictionary<string, string>
            {
                ["userName"] = userName,
                ["password"] = password
            };

            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient
                        .PostAsync(@"http://localhost:58428/api/v1/Authorization/Login", content);

                    var result = await response.Content.ReadAsStringAsync();
                    var auth = JsonConvert.DeserializeObject<AuthDTO>(result);
                    if (auth != null)
                    {
                        Token = auth.Access_Token;
                    }
                    return auth;
                }
            }
        }


        public static async Task<AuthDTO> Create(UserDTO dto)
        {

            var postData = new Dictionary<string, string>
            {
                ["userName"] = dto.UserName,
                ["password"] = dto.Password
            };

            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient
                        .PostAsync(@"http://localhost:58428/api/v1/Authorization/Create", content);

                    var result = await response.Content.ReadAsStringAsync();
                    var auth = JsonConvert.DeserializeObject<AuthDTO>(result);
                    if (auth != null)
                    {
                        Token = auth.Access_Token;
                    }
                    return auth;
                }
            }
        }

        public static string Token { get; private set; }
    }
}
