using AutoMapper;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using Microsoft.AspNetCore.Mvc.Versioning;
using Marvin.Cache.Headers;
using AspNetCoreRateLimit;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Services.Contracts.AcademcianService;
using Services.AcademicianManagers;
using Services.ClubLeaderManagers;
using Services.Contracts.ClubLeaderService;
using Services.Contracts.UsersService;
using Services.UsersManagers;
using Services.Contracts.AdminService;
using Services.AdminManagers;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerService, LoggerManager>();

        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<LogFilterAttribute>();
            services.AddScoped<ValidateMediaTypeAttribute>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithExposedHeaders("X-Pagination"));
            });
        }

        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<EventDto>, DataShaper<EventDto>>();
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config.OutputFormatters
                    .OfType<SystemTextJsonOutputFormatter>()
                    .FirstOrDefault();

                if (systemTextJsonOutputFormatter != null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.medeniyet.hateoas+json");
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.medeniyet.apiroot+json");
                }

                var xmlOutputFormatter = config.OutputFormatters
                    .OfType<XmlDataContractSerializerOutputFormatter>()
                    .FirstOrDefault();

                if (xmlOutputFormatter != null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.medeniyet.hateoas+xml");
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.medeniyet.apiroot+xml");
                }
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        public static void ConfigureResponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching();
        }

        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders(
                expirationOpt =>
                {
                    expirationOpt.CacheLocation = CacheLocation.Private;
                    expirationOpt.MaxAge = 1;
                },
                validationOpt =>
                {
                    validationOpt.MustRevalidate = false;
                });
        }

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 60,
                    Period = "1m"
                }
            };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireDigit = true;

                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Club's Manager",
                    Version = "v1",
                    Description = "Club's Manager ASP.NET Core Web API",
                    TermsOfService = new Uri("https://github.com/ysfkc2113"),
                    Contact = new OpenApiContact
                    {
                        Name = "Yusuf Koç",
                        Email = "ysfkc2113@gmail.com",
                        Url = new Uri("https://github.com/ysfkc2113")
                    }
                });

                s.SwaggerDoc("v2", new OpenApiInfo { Title = "Club's Manager", Version = "v2" });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClubRepository, ClubRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IClubUserRepository, ClubUserRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            // Core services
            services.AddScoped<IClubService, ClubManager>();
            services.AddScoped<IClubUserService, ClubUserManager>();
            services.AddScoped<IUsersService, UsersManager>();
            services.AddScoped<IEventService, EventManager>();
            services.AddScoped<IUserRoleService, UserRoleManager>();
            services.AddScoped<IAuthenticationService, AuthenticationManager>();

            // Academician services
            services.AddScoped<IEventServiceAcademician, EventManagerAcademician>();
            services.AddScoped<IClubServiceAcademician, ClubManagerAcademician>();
            services.AddScoped<IClubUserServiceAcademician, ClubUserManagerAcademician>();
            services.AddScoped<IUsersServiceAcademician, UsersManagerAcademician>();
            services.AddScoped<IUserRoleServiceAcademician, UserRoleManagerAcademician>();

            // ClubLeader services
            services.AddScoped<IEventServiceClubLeader, EventManagerClubLeader>();
            services.AddScoped<IClubServiceClubLeader, ClubManagerClubLeader>();
            services.AddScoped<IUsersServiceClubLeader, UsersManagerClubLeader>();
            services.AddScoped<IClubUserServiceClubLeader, ClubUserManagerClubLeader>();

            // Users services
            services.AddScoped<IClubUserServiceUsers, ClubUserManagerUsers>();
            services.AddScoped<IUserServiceUsers, UserManagerUsers>();
            services.AddScoped<IEventServiceUsers, EventManagerUsers>();
            services.AddScoped<IClubServiceUsers, ClubManagerUsers>();

            // Lazy resolver
            services.AddScoped(typeof(Lazy<>), typeof(LazyResolver<>));
        }
    }
}
