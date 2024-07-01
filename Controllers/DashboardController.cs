using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
namespace Profile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController: ControllerBase
{
    public readonly IDistributedCache _distCache;
    public DashboardController(IDistributedCache distCache)
    {
        _distCache = distCache;
    }

    [HttpGet(Name = "GetProfileDist")]
    // [Authorize]
    public async Task<IEnumerable<ProfileDemo>> Get()
    {
        var cacheentryoptions=new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        };
        var profile1 =await _distCache.GetStringAsync("ProfileDist");

         if(profile1==""){
         var profile = ProfileDetails.getProfileData();
         await _distCache.SetStringAsync("ProfileDist",Convert.ToString(profile),cacheentryoptions);
        return profile;
        }
        else{
           return JsonSerializer.Deserialize<IEnumerable<ProfileDemo>>(profile1);
        } 
    }
}