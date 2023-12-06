using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.WebAPIAuthServices;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IClientAuthService authService;
    private readonly IAdminAuthService adminAuth;

    public AuthController(IConfiguration config, IClientAuthService authService, IAdminAuthService adminAuth)
    {
        this.config = config;
        this.authService = authService;
        this.adminAuth = adminAuth;
    }
    
    private List<Claim> GenerateClaimsClient(Client client)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, client.username),
        };
        return claims.ToList();
    }
    
    private List<Claim> GenerateClaimsAdmin(Administrator admin)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, admin.username),
            new Claim("Domain", admin.emailDomain.ToString()),
        };
        return claims.ToList();
    }
    
    private string GenerateJwtClient(Client client)
    {
        List<Claim> claims = GenerateClaimsClient(client);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    
    private string GenerateJwtAdmin(Administrator admin)
    {
        List<Claim> claims = GenerateClaimsAdmin(admin);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    
    [HttpPost, Route("login")]
    public async Task<ActionResult> LoginClient([FromBody] ClientLoginDTO userLoginDto)
    {
        try
        {
            Client user = await authService.GetClientAsync(userLoginDto.Username, userLoginDto.Password);
            string token = GenerateJwtClient(user);
            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost, Route("loginAdmin")]
    public async Task<ActionResult> LoginAdmin([FromBody] ClientLoginDTO userLoginDto)
    {
        try
        {
            Administrator admin = await adminAuth.GetAdminAsync(userLoginDto.Username, userLoginDto.Password);
            string token = GenerateJwtAdmin(admin);
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    

}