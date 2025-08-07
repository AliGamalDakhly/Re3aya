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
using System.Text;

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
            builder.Services.AddAutoMapper(typeof(DoctorProfile).Assembly);

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
            builder.Services.AddScoped<IDoctorTimeSlotService, DoctorTimeSlotService>();
            builder.Services.AddScoped<ISystemInfoService, SystemInfoService>();


            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
            builder.Services.AddScoped<IAiService, AiService>();
            builder.Services.AddScoped<IMedicalSuggest, MedicalSuggest>();
            builder.Services.AddScoped<IRagService, RagService>();



            // Add CORS services before building the app
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


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
                    ValidateAudience = false,    //it make error
                    ValidateLifetime = true, // check expiration
                    ValidateIssuerSigningKey = true,


                    ValidIssuer = builder.Configuration["JWT:IssuerIP"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))



                };

            });

            #region Customize swagger to test Authorization

            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = " ITI Projrcy"
                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by your JWT token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                      new OpenApiSecurityScheme{Reference = new OpenApiReference
                      {
                          Type = ReferenceType.SecurityScheme,Id = "Bearer"}
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


            // seeding roles here instead of adding each role in each service 
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                DefaultRolesSeeder.SeedRolesAsync(roleManager).GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}