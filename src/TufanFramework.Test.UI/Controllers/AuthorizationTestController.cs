using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TufanFramework.Test.UI.Controllers
{
    public class AuthorizationTestController : Controller
    {
        public AuthorizationTestController()
        {
        }

        [HttpGet]
        [Route("GetAB")]
        [Authorize(Roles = "A,B")]
        public IActionResult GetAB()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetD")]
        [Authorize(Roles = "D")]
        public IActionResult GetD()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetAuthorize")]
        [Authorize]
        public IActionResult GetAuthorize()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetAnonymous")]
        public IActionResult GetAnonymous()
        {
            return Ok();
        }
    }
}