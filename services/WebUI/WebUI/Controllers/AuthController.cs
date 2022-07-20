using Microsoft.AspNetCore.Mvc;
using WebUI.Configurations;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly Auth0Settings _auth0Settings;
    
    public AuthController(Auth0Settings auth0Settings)
    {
        _auth0Settings = auth0Settings;
    }
    
    [HttpGet]
    public ActionResult<Auth0Settings> GetConfig()
    {
        return Ok(_auth0Settings);
    }
}