using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace poc.auth.service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Require Authenticated users by default...
            services.AddControllers(config => {
                var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //register the handlers...
            services.AddScoped<CookieAuthenticationEventsHandler>();
            services.AddScoped<OpenIdConnectEventsHandler>();
            
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/authentication/bam";
                     //options.Cookie.Name = "authentication.localdev.com";
                    //options.Cookie.Domain = "localdev.com";
                    //options.EventsType = typeof(CookieAuthenticationEventsHandler);
                    options.Events.OnSignedIn = ctx =>
                    {
                        //maybe some action/logic to perform here 
                        return Task.CompletedTask;
                    };

                    options.Events.OnRedirectToLogin = ctx =>
                    {
                        //maybe some action/logic to perform here 
                        return Task.CompletedTask;
                    };
                })
                .AddOpenIdConnect(options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.ClientId = "888646173334-jmjm9f5inikcmm5i7ivbp73piqsc12fn.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                    options.Authority = "https://accounts.google.com";
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    
                    options.SaveTokens = false;
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.EventsType = typeof(OpenIdConnectEventsHandler);

                    //options.Events.OnTokenValidated = ctx =>
                    //{
                    //    ClaimsIdentity claimsIdentity = new ClaimsIdentity();

                    //    //Get Roles for user from datastore
                    //    var roles = new[] { "admin", "superuser", "content-manager" };

                    //    foreach (var role in roles)
                    //    {
                    //        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                    //    }

                    //    ctx.Principal.AddIdentity(claimsIdentity);

                    //    return Task.CompletedTask;
                    //};
                }
            );

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "poc_auth_service", Version = "v1" });
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "poc_auth_service v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers()
            );
        }
    }

    public class CookieAuthenticationEventsHandler : CookieAuthenticationEvents
    {
        public override Task SignedIn(CookieSignedInContext context)
        {
            return base.SignedIn(context);
        }

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.RedirectUri = "/Personal/Login";

            return base.RedirectToLogin(context);
        }
    }

    public class OpenIdConnectEventsHandler : OpenIdConnectEvents
    {
        public override Task TokenValidated(TokenValidatedContext context)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();

            //Get Roles for user from datastore
            var roles = new[] { "admin", "superuser", "content-manager" };

            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            context.Principal.AddIdentity(claimsIdentity);

            return base.TokenValidated(context);
        }
    }

    //public class MyClaimsTransformation : IClaimsTransformation
    //{
    //    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    //    {
    //        ClaimsIdentity claimsIdentity = new ClaimsIdentity();

    //        //Get Roles for user from datastore
    //        var roles = new []{ "admin", "superuser", "content-manager" };

            //foreach(var role in roles)
            //{
            //    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            //}

    //        principal.AddIdentity(claimsIdentity);
    //        return Task.FromResult(principal);
    //    }
    //}
}
