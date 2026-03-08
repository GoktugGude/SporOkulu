using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SporOkulu.Application;
using SporOkulu.Application.DTOs.CoachDTOs;
using SporOkulu.Application.Interfaces;
using SporOkulu.Application.Services;
using SporOkulu.Domain;
using SporOkulu.Domain.Entities;
using SporOkulu.Domain.Interfaces;
using SporOkulu.Infrastructure.Context;
using SporOkulu.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Controller
builder.Services.AddControllers();

//FluenApi
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();

//EF
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default")));

// 3. IDENTITY KAYDI (Burası Eksikti!)
builder.Services.AddIdentity<AppUser, AppRole>(options => {
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


// 4. GENERIC REPOSITORY VE SERVICE KAYITLARI (Burası Eksikti!)
//Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICoachRepository, CoachRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IParentRepository, ParentRepository>();


//Services
builder.Services.AddScoped(typeof(IGenericService<,,,>), typeof(GenericManager<,,,>));
builder.Services.AddScoped<IStudentService, StudentManager>();
builder.Services.AddScoped<IPaymentService, PaymentManager>();
builder.Services.AddScoped<ICoachService, CoachManager>();
builder.Services.AddScoped<IAttendanceService, AttendanceManager>();
builder.Services.AddScoped<IParentService, ParentManager>();
// // Coach için Generic Service kaydı
// builder.Services.AddScoped<IGenericService<Coach, DetailCoachDto, CreateCoachDto, UpdateCoachDto>, GenericManager<Coach, DetailCoachDto, CreateCoachDto, UpdateCoachDto>>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 6. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Vite'ın varsayılan portu
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors("AllowReact");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();
app.Run();

