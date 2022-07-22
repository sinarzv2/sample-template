using Microsoft.AspNetCore.Mvc;

namespace SampleTemplate.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
