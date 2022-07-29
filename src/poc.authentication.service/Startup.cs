using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using System.Threading.Tasks;

namespace poc.authentication.service
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
            services.AddMvc(config =>
            {
                config.EnableEndpointRouting = false;
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
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login/";

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
                    //options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
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
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseRouting();

            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                RequireHeaderSymmetry = false,
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
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
}
