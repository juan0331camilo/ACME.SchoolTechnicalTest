namespace ACME.School.WebApi.Bootstrapper
{
    using ACME.School.Application.Interfaces;
    using ACME.School.Application.Services;
    using ACME.School.Domain.Data;
    using ACME.School.Domain.Interfaces;
    using ACME.School.Domain.Repositories;
    using FluentValidation;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// App Builder 
    /// </summary>
    public static class AppBuilder
    {
        /// <summary>
        /// Get App
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static WebApplication GetApp(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHealthChecks();
            MemoryCache memoryCache = new(new MemoryCacheOptions { SizeLimit = 30 });
            builder.Services.AddSingleton<IMemoryCache>(memoryCache);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped);
            builder.Services.AddDbContext<ACMEContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ACMEContext")));

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ACME school Service",
                    Description = "Should provide the management of their courses and students.",
                    Contact = new OpenApiContact
                    {
                        Name = "Microsoft Learn",
                        Url = new Uri("https://learn.microsoft.com/")
                    }
                });

                // Configurar el esquema de seguridad para el Bearer Token
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Introduce el token JWT de esta forma: Bearer {token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // Incluir el archivo XML de comentarios si está habilitado
                string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,//Importante tener en cuenta.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddAuthorization();

            // Services
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();

            // Logs
            builder.Logging.ClearProviders();

            WebApplication app = builder.Build();
            return app;
        }
    }
}
