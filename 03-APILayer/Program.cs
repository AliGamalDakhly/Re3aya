using _01_DataAccessLayer.Data.Context;
using _01_DataAccessLayer.Repository.GenericRepository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.Mapping;
using _02_BusinessLogicLayer.Service.IServices;
using _02_BusinessLogicLayer.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace _03_APILayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            builder.Services.AddDbContext<Re3ayaDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Re3ayaDbConnectionString")));

            //Register AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //Register Services      //add your services here
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<ISpecializationService, SpecialzationService>();
            builder.Services.AddScoped<IAddressService, AddressService>();

            
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
