using Microsoft.AspNetCore.Mvc;
using WebUI.Configurations;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigController : ControllerBase
{
    private readonly ServicesConfiguration _servicesConfiguration;

    public ConfigController(ServicesConfiguration servicesConfiguration)
    {
        _servicesConfiguration = servicesConfiguration;
    }
    
    [HttpGet]
    public ActionResult<ServicesConfiguration> GetConfig()
    {
        return Ok(_servicesConfiguration);
    }
}