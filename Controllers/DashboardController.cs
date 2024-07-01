using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
namespace Profile.Controllers;

[ApiController]
[Route("api/[controller]/[action]/{id?}")]
public class DashboardController: ControllerBase
{
    public readonly IDistributedCache _distCache;
    public readonly IDataProtector _Protector;
    public DashboardController(IDistributedCache distCache,IDataProtectionProvider Provider)
    {
        _distCache = distCache;
        _Protector=Provider.CreateProtector("MyPurpose.StringProtection");
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

    [HttpGet(Name = "DetailDist")]
    public IEnumerable<ProfileDemo> DetailDist()
    {
         var profile = ProfileDetails.getProfileData();
        return profile;
    }

    [HttpPost]
    public string Encrypt(string Name)
    {
        return _Protector.Protect(Name);
    }

    [HttpPost]
    public string Decrypt(string EncName)
    {
        return _Protector.Unprotect(EncName);
    }

}