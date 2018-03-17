using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Auth;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BarNone.DataLift.APIRequest
{
    /// <summary>
    /// Handels getting tokens to validate a users privliges
    /// </summary>
    public class TokenManager
    {
        /// <summary>
        /// Authorizes an existing user
        /// </summary>
        /// <param name="userName">Username of a user</param>
        /// <param name="password">Password matching the username</param>
        /// <returns>Authorization results</returns>
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
                        .PostAsync($"http://{Endpoint<UserDTO>.BASE_URL}/api/v1/Authorization/Login", content);

                    var result = await response.Content.ReadAsStringAsync();
                    var auth = JsonConvert.DeserializeObject<AuthDTO>(result);
                    if (auth != null)
                    {
                        Token = auth.Access_Token;
                    }
                    else
                    {
                        auth = new AuthDTO { Authorized = false };
                    }
                    return auth;
                }
            }
        }

        /// <summary>
        /// Creates and authorizes a new user
        /// </summary>
        /// <param name="dto">New User being added</param>
        /// <returns>The result of the create, if valid the user will be authorized</returns>
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
                        .PostAsync($"http://{Endpoint<UserDTO>.BASE_URL}/api/v1/Authorization/Create", content);

                    var result = await response.Content.ReadAsStringAsync();
                    var auth = JsonConvert.DeserializeObject<AuthDTO>(result);
                    if (auth != null)
                    {
                        Token = auth.Access_Token;
                    }
                    else
                    {
                        auth = new AuthDTO { Authorized = false };
                    }
                    return auth;
                }
            }
        }

        /// <summary>
        /// Token of the currently authorized session
        /// </summary>
        public static string Token { get; private set; }
    }
}
