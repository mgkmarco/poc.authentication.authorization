using CustomIdentityProviderSample.CustomProvider;
using CustomIdentityProviderSample.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Data.SqlClient;

namespace CustomIdentityProviderSample
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
            // Add identity types
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultTokenProviders();

            // Identity Services
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, CustomRoleStore>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddTransient(e => new SqlConnection(connectionString));
            services.AddTransient<DapperUsersTable>();

            //services.AddDataProtection()
            //    .PersistKeysToFileSystem(new DirectoryInfo(@"c:\PATH TO COMMON KEY RING FOLDER"))
            //    .SetApplicationName("SharedCookieApp");

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNet.SharedCookie";
                //options.Cookie.Domain = ".localdev.com";
            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
               //.AddCookie(options => options.Cookie.Name = "authentication.portal")
               .AddGoogle(options =>
                {
                    options.ClientId = "888646173334-jmjm9f5inikcmm5i7ivbp73piqsc12fn.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                })
               .AddOpenIdConnect(options =>
                {
                    //options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.CallbackPath = "/Account/ExternalLoginCallback";
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.ClientId = "888646173334-jmjm9f5inikcmm5i7ivbp73piqsc12fn.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                    options.Authority = "https://accounts.google.com";
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.SaveTokens = false;
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    //options.EventsType = typeof(OpenIdConnectEventsHandler);

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

            services.AddMvc(x =>  x.EnableEndpointRouting = false);

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseAuthentication();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
