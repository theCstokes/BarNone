using TheRack.Core;
using TheRack.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;

namespace TheRack.ResourceServer
{
    public class RequestParser
    {
        public static DomainParameters GetParams(HttpRequestMessage request)
        {
            ClaimsPrincipal principal = request.GetRequestContext().Principal as ClaimsPrincipal;

            return principal.Claims.Aggregate(new DomainParameters(), (dp, claim) =>
            {
                if (DomainParameters.DataMap.ContainsKey(claim.Type))
                {
                    var setter = DomainParameters.DataMap[claim.Type];
                    setter(dp, claim.Value);
                }
                return dp;
            });
        }
    }
}