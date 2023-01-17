using Microsoft.EntityFrameworkCore;
using Ruper.BLL.Mapping;
using Ruper.BLL;
using Ruper.DAL.DataContext;
using Ruper.DAL;
using FluentValidation.AspNetCore;
using Ruper.BLL.Dtos;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace Ruper.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin().AllowAnyMethod();
                                  });
            });

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(o =>
            //{
            //    o.RequireHttpsMetadata = false;
            //    o.SaveToken = false;
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero,
            //        ValidIssuer = builder.Configuration["JWT:Issuer"],
            //        ValidAudience = builder.Configuration["JWT:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
            //    };
            //});

            builder.Services.AddFluentValidation(x=>x.RegisterValidatorsFromAssembly(assembly:Assembly.GetExecutingAssembly()));
            
            //builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            //builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddDalServices();
            builder.Services.AddBllServices();

            var app = builder.Build();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")),
                RequestPath = new PathString("/images"),
                EnableDirectoryBrowsing = true
            });

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}