using Microsoft.AspNetCore.Mvc;

namespace Profile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController: ControllerBase
{
    [HttpGet(Name = "GetProfile")]
    public IEnumerable<ProfileDemo> Get()
    {
        return ProfileDetails.getProfileData();
    }

    [HttpPost(Name = "PostProfile")]
    public IActionResult Post(int Id,string info)
    {
        var ProfileDemo=new ProfileDemo();
        ProfileDemo.AddInfo.Add(new AdditionalInformation(){Id=Id,Info=info});
        return Ok(true);
    }
}
