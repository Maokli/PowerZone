using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using API.Extensions;
using AutoMapper;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Services;

namespace API
{
  public class Startup
  {
    private readonly IConfiguration _configuration;
    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                        builder =>
                        {
                             builder.WithOrigins("http://localhost:4200/")
                             .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .SetIsOriginAllowed((host) => true);
                        });
        });

      services.AddControllers();
      services.AddDbContext<IdentityContext>(x =>
      x.UseSqlServer(_configuration.GetConnectionString("Sql")));
      services.AddScoped<ITokenService, TokenService>();





      services.AddIdentityCore<Client>()
      .AddSignInManager<SignInManager<Client>>()
       .AddRoles<IdentityRole>()
       .AddEntityFrameworkStores<IdentityContext>()
       .AddDefaultTokenProviders();

      services.AddMvc();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(opt =>
      {
        opt.TokenValidationParameters =
         new Microsoft.IdentityModel.Tokens.TokenValidationParameters
         {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
           GetBytes(_configuration["Token:Key"])),
           ValidIssuer = _configuration["Token:Issuer"],
           ValidateIssuer = false,
           ValidateAudience = false
         };
      });


      services.AddAutoMapper(typeof(ClientProfile));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors(MyAllowSpecificOrigins);

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
