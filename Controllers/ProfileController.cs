using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Profile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController: ControllerBase
{
    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public IActionResult Login([FromForm]JwtAuthRequest Request)
    {
        var AuthMgr = new JwtAuthManager();
        var result = AuthMgr.Authenticate(Request.username,Request.password);
        if(result==null)
            return Unauthorized();
        else
            return Ok(result);
    }

    [HttpGet(Name = "GetProfile")]
    [Authorize]
    public IEnumerable<ProfileDemo> Get()
    {
        return ProfileDetails.getProfileData();
    }

    [HttpPost(Name = "PostProfile")]
    [Authorize]
    public IActionResult Post(int Id,string info)
    {
        var ProfileDemo=new ProfileDemo();
        ProfileDemo.AddInfo.Add(new AdditionalInformation(){Id=Id,Info=info});
        return Ok(true);
    }

    
}
