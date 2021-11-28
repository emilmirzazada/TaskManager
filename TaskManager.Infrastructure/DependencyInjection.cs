using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Settings;
using TaskManager.Infrastructure.Services;
using TaskManager.Persistence;
using TaskManager.Persistence.Identity;

namespace TaskManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IWebHostEnvironment environment,IConfiguration config)
        {

            if (environment.IsEnvironment("Test"))
            {
                /*services.AddIdentityServer()
                    .AddApiAuthorization<AppUser, ApplicationDbContext>(options =>
                    {
                        options.Clients.Add(new Client
                        {
                            ClientId = "TaskManager.IntegrationTests",
                            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                            ClientSecrets = { new Secret("secret".Sha256()) },
                            AllowedScopes = { "TaskManager.WebUIAPI", "openid", "profile" }
                        });
                    })
                    .AddTestUsers(new List<TestUser>
                    {
                        new TestUser
                        {
                            SubjectId = "4c7c3792-0d6f-44e4-8d35-9d0df37178f3",
                            Username = "admin@smartsolutions",
                            Password = "Emil!",
                            Claims = new List<Claim>
                            {
                                new Claim(JwtClaimTypes.Email, "admin@smartsolutions")
                            }
                        }
                    });*/
            }
            else
            {
                services.Configure<MailSettings>(config.GetSection("MailSettings"));
                services.AddTransient<IEmailService, EmailService>();
                services.AddTransient<IDateTimeService, DateTimeService>();
                services.AddTransient<IIdentityService, IdentityService>();
            }

            services.AddAuthentication();

            return services;
        }
    }
}
