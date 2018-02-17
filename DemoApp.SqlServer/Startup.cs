using System;
using DbManager.Interface;
using DbManager.Schema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DemoApp.SqlServer
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
            services.AddOptions();
            services.AddSqlServerDbManager(options =>
            {
                options.ConnectionString = Configuration["DbManagerOptions:ConnectionString"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbManager dbManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                var repo = dbManager.Repository<MyTableSet>();
                repo.Create(new MyTableSet(){ Description = "Hello World!", CreateDate = DateTime.Now});
                repo.SaveChanges();

                var content = JsonConvert.SerializeObject(repo.Read());
                await next.Invoke();
                await context.Response.WriteAsync(content);
            });
        }
    }
}
