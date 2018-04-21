using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using BarNone.TheRack.ResourceServer.API;
using BarNone.Shared.DataTransfer;
using BarNone.TheRack.Repository;
using BarNone.TheRack.DataAccess;
using BarNone.Shared.DataConverters;

namespace TheRack.ResourceServer.API.Controllers
{
    /// <summary>
    /// Controller for authentication endpoints.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/v1/[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationController"/> class.
        /// </summary>
        /// <param name="jwtOptions">The JWT options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public AuthorizationController(IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);

            _logger = loggerFactory.CreateLogger<AuthorizationController>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        /// <summary>
        /// Logins the specified application user.
        /// </summary>
        /// <param name="applicationUser">The application user.</param>
        /// <returns>Token</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] UserDTO applicationUser)
        {
            var identity = await GetClaimsIdentity(applicationUser);
            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({applicationUser.UserName}) or password ({applicationUser.Password})");

                return BadRequest(JsonConvert.SerializeObject(new
                {
                    authorized = false,
                    expires_in = 0
                }, _serializerSettings));
            }

            await NotificationWebSocketMiddleware.NotifyAll($"{applicationUser.Name} has logged in.");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                ClaimValueTypes.Integer64),
                identity.FindFirst("User"),
                identity.FindFirst("UserID")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            return new OkObjectResult(JsonConvert.SerializeObject(new
            {
                authorized = true,
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            }, _serializerSettings));
        }

        /// <summary>
        /// Creates the specified application user.
        /// </summary>
        /// <param name="applicationUser">The application user.</param>
        /// <returns>Token</returns>
        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] UserDTO applicationUser)
        {
            applicationUser.Name = applicationUser.UserName;

            ClaimsIdentity identity = null;

            //using (var dc = new DomainContext())
            //{
            //var repo = new UserRepository(dc);
            //var user = repo.Create(applicationUser);
            //dc.SaveChanges();


            var user = DataAccessAuthenticator.Create(
                Converters.NewConvertion().User.CreateDataModel(applicationUser),
                "salt",
                applicationUser.Password);

            Converters.NewConvertion().User.CreateDTO(user);
            identity = await GetClaimsIdentity(applicationUser);
            //}

            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({applicationUser.UserName}) or password ({applicationUser.Password})");
                return BadRequest("Invalid credentials");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                ClaimValueTypes.Integer64),
                identity.FindFirst("User"),
                identity.FindFirst("UserID")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            var response = new
            {
                authorized = true,
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        /// <summary>
        /// IMAGINE BIG RED WARNING SIGNS HERE!
        /// You'd want to retrieve claims through your claims provider
        /// in whatever way suits you, the below is purely for demo purposes!
        /// </summary>
        private static Task<ClaimsIdentity> GetClaimsIdentity(UserDTO user)
        {
            var entity = DataAccessAuthenticator.Login(user.UserName, user.Password);

            if (entity == null)
            {
                //return Task.FromResult(new ClaimsIdentity(
                //  new GenericIdentity(user.UserName, "Token"),
                //  new Claim[] { }));
                return Task.FromResult<ClaimsIdentity>(null);
            }

            return Task.FromResult(new ClaimsIdentity(
              new GenericIdentity(user.UserName, "Token"),
              new[]
              {
                new Claim("User", "IAmMickey"),
                new Claim("UserID", Convert.ToString(entity.ID))
              }));

        }
    }
}
