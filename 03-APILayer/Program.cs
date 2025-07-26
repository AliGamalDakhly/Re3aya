using System.Text;
using _01_DataAccessLayer.Data.Context;
using _01_DataAccessLayer.Data.Seed;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository.GenericRepository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.Mapping;
using _02_BusinessLogicLayer.Service.IServices;
using _02_BusinessLogicLayer.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace _03_APILayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            builder.Services.AddDbContext<Re3ayaDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Re3ayaDbConnectionString")));


            builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<Re3ayaDbContext>()
            .AddDefaultTokenProviders();


            //Register AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddAutoMapper(typeof(DoctorProfile));
            builder.Services.AddAutoMapper(typeof(AccountMappingProfile));


            //Register Services      //add your services here
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<ISpecializationService, SpecialzationService>();
            builder.Services.AddScoped<IAddressService, AddressService>();

            builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();

            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IRatingService, RatingService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddHttpClient<IPaymobService, PaymobService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();




            // Add JWT authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true; //Not Expired?
                options.RequireHttpsMetadata = false; //specfic Protocol Https

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, // check expiration
                    ValidateIssuerSigningKey = true,


                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))

                };

            });

            #region Customize swagger to test Authorization

            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Re3aya API",
                    Description = "ITI Project API"
                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token.\n\nExample: \"Bearer eyJhbGciOi...\""
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
            });

            #endregion

            // Add Authorization
            builder.Services.AddAuthorization();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Middleware global error handler
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Unhandled Exception: " + ex.Message);
                }
            });

           
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
