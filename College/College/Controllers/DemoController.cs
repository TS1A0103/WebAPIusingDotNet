using College.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //2.Loosely coupled technique
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;

        }
        [HttpGet]
        public ActionResult Index()
        {
            _logger.LogTrace("LogMessage from trace");
            _logger.LogDebug("LogMessage from Debug");
            _logger.LogInformation("LogMessage from Information");
            _logger.LogWarning("LogMessage from LogWarning");
            _logger.LogError("LogMessage from LogError");
            _logger.LogCritical("LogMessage from LogCritical");
            return Ok();
        }
    }
}
