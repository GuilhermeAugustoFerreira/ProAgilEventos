﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProAgil.Repository;
//using ProAgil.API.Data;

namespace ProAgil.API
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
            services.AddDbContext<ProAgilContext>(x => x.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            /*Acessar Nuget pelo VsCode:
            Install Nuget Gallery from extension marketplace.
            Launch from the menu bar View > Command Palette or ⇧⌘P (Ctrl+Shift+P on Windows and Linux). Type Nuget: Open Gallery .
            The GUI above is displayed. You can filter just like in regular Visual Studio.
            Make sure the .

            MySql: Pomelo.EntityFrameworkCore.MySql

            DefaultConnection: no arquivo Appsettings.developments.json, inserir mais um parametro:
            "ConnectionStrings":
            {
                "DefaultConnection": "Data Source = ..."
            }
            */
            services.AddScoped<IProAgilRepository, ProAgilRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();//configuração de permissao de requisição cruzada
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //doapp.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());//essa linha tem necessariamente que ficar antes da "app.UseMvc();"
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
