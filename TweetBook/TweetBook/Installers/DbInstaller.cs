using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweetBook.Data;
using TweetBook.Services;

namespace TweetBook.Installers
{
    public class DbInstaller :IInstaller

    {
        public DbInstaller()
        {
        }

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // 현재, Scoped lifecycle
            services.AddDbContext<DataContext>(options =>
               options.UseSqlite(
                   configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IPostService, PostService>();
        }
    }
}

