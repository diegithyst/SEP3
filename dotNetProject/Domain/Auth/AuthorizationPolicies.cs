using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;


namespace Domain.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
           options.AddPolicy("MustBeAdministrator", a =>
               a.RequireAuthenticatedUser().RequireClaim("emailDomain", "admin"));
        });
    }
}