using System.Web.Http;
using TheRack.AuthorizationServer.DataTransfer;
using TheRack.AuthorizationServer.DomainModel;
using TheRack.AuthorizationServer.DomainLogic;

namespace TheRack.AuthorizationServer.Controllers
{
    [RoutePrefix("api/v1/audience")]
    public class AudienceController : ApiController
    {
        [Route("")]
        public IHttpActionResult Post(AudienceDTO audienceModel)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Audience newAudience = AudiencesStore.AddAudience(audienceModel.Name);

            return Ok<Audience>(newAudience);

        }
    }
}
