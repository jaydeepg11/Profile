using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
public class JwtAuthManager
{
    public JwtAuth Authenticate(string username,string password)
    {
        if(username!="jaydeep" && password!="jaydeep")
        {
            return null;
        }

        var jwtSecurity = new JwtSecurityTokenHandler();
        var expire= DateTime.Now.AddMinutes(20);
        var Token = Encoding.ASCII.GetBytes("SecurityToken123");
        var SecurityTokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim("username",username),
                new Claim(ClaimTypes.PrimaryGroupSid,"new Group")
            }),
            Expires = expire,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Token),SecurityAlgorithms.HmacSha256Signature)
        };

        var SecurityToken = jwtSecurity.CreateToken(SecurityTokenDescriptor);
        var token =jwtSecurity.WriteToken(SecurityToken);

        return new JwtAuth
        {
            Token = token,
            Username=username,
            Expires_in=(int)expire.Subtract(DateTime.Now).TotalSeconds
        };

    }
}