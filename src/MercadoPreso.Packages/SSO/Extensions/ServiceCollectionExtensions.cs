using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SSO.Context;

namespace SSO.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddSSODbContext(IConfiguration configuration)
        {
            services.AddDbContext<SSODbContext>(options =>
            {
                options.UseMongoDB(configuration.GetConnectionString("mongodb"));
                options.UseOpenIddict();
            });
            return services;
        }

        public IServiceCollection AddSSOAuthentication()
        {
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.Cookie.Name = "sso.session";
                });
                return services;
        }

        public IServiceCollection AddSSOAuthorization()
        {
            services
                .AddOpenIddict()
                //configura as entidades necessarias pra validar o cliente
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore().UseDbContext<SSODbContext>();
                })
                .AddServer(options =>
                {
                    //vai assinar o access token com chave privada e vai encriptografar
                    options.AddDevelopmentEncryptionCertificate()
                           .AddDevelopmentSigningCertificate();

                    //habilita as rotas de token e authorize
                    options.SetTokenEndpointUris("connect/token")
                           .SetAuthorizationEndpointUris("connect/authorize")
                           .SetUserInfoEndpointUris("connect/userinfo");

                    //habilita refresh token
                    options.AllowRefreshTokenFlow();
                    //isso aki adiciona pkce no flow de authorize
                    options.AllowAuthorizationCodeFlow()
                           .RequireProofKeyForCodeExchange();
                    //permite com que possamos customizar a logica que ocorre nos endpoints /connect/token /authorize/token
                    options
                        .UseAspNetCore() //integra o openiddict ao pipeline http do aspnetcore
                        .EnableTokenEndpointPassthrough()
                        .EnableAuthorizationEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    //valida localmente pois emitimos daqui
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });

            return services;
        }
    }
}
