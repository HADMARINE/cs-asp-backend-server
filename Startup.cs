using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cs_asp_backend_server.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace cs_asp_backend_server
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
            Credentials.Instance.Initialize(Configuration);

            services.AddAuthentication("OAuth")
            .AddJwtBearer(
                "OAuth",
                options =>
                {
                    byte[] passwordByteArray = Encoding.UTF8.GetBytes(Credentials.Instance.TOKEN_KEY);

                    var key = new SymmetricSecurityKey(passwordByteArray);

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Headers.ContainsKey("Authorization"))
                            {
                                context.Token = context.Request.Headers["Authorization"];
                            }
                            else if (context.Request.Cookies.ContainsKey("Authorization"))
                            {
                                context.Token = context.Request.Cookies["Authorization"];
                            }
                            else if (context.Request.Query.ContainsKey("authorization"))
                            {
                                context.Token = context.Request.Query["authorization"];
                            }

                            return Task.CompletedTask;
                        }
                    };

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Constants.Issuer,
                        ValidAudience = Constants.Audience,
                        IssuerSigningKey = key
                    };
                }
            );
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Constants.AppName, Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    // https://cpratt.co/customizing-swagger-ui-in-asp-net-core/
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Constants.AppName} v1");
                    c.RoutePrefix = "docs/api";
                    c.DocumentTitle = $"{Constants.AppName}";
                });
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
