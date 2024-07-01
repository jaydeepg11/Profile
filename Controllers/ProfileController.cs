using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
namespace Profile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController: ControllerBase
{

public readonly IMemoryCache _memory;
    public ProfileController(IMemoryCache memory)
    {
        _memory=memory;
    }

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

        
        var profile1 =_memory.TryGetValue("Profile",out IEnumerable<ProfileDemo> data);

         if(profile1==false){
         var profile = ProfileDetails.getProfileData();
        _memory.Set("Profile",profile);
        return profile;
        }
        else{
            return data;
        } 
    }

    [HttpPost(Name = "PostProfile")]
    [Authorize]
    public IActionResult Post(int Id,string info)
    {
        return Ok(ProfileDetails.AddInfoDetails(Id,info));
    }    
}
