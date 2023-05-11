using ApptiqueServer.Config;
using ApptiqueServer.Services;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using MudBlazor.Services;

namespace ApptiqueServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddTransient<IAppService, AppService>();
            builder.Services.AddMudServices();
            builder.Services.Configure<SecretModel>(
            builder.Configuration.GetSection("Secret"));

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddHttpClient();
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("Secret"));
            //builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });


            var app = builder.Build();



            var env = app.Services.GetService<IWebHostEnvironment>();


            var config = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            //// Set up custom content types - associating file extension to MIME type
            //var provider = new FileExtensionContentTypeProvider();
            //// Add new mappings
            //provider.Mappings[".apk"] = "application/x-msdownload";
            //provider.Mappings[".htm3"] = "text/html";
            //provider.Mappings[".image"] = "image/png";

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    ContentTypeProvider = provider
            //});

            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".apk"] = "application/vnd.android.package-archive";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            app.UseRouting();

            app.MapBlazorHub();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapFallbackToPage("/_Host");
            });
            app.Run();
        }
    }
}