using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TweetBook.Installers
{
    public class MvcInstaller :IInstaller
    {
        public MvcInstaller()
        {
        }

        public void InstallServices(IServiceCollection services, IConfiguration configutation)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TweetBook API", Version = "v1" });
            });
        }
    }
}

