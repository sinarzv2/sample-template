using Microsoft.AspNetCore.Mvc;

namespace SinaRazavi_Test.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
