using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using EmploymentExchangeAPI;
using EmploymentExchangeAPI.Data;
using EmploymentExchangeAPI.Middlewares;
using EmploymentExchangeAPI.Repositories;
using System.Text.Json.Serialization;
using System.Text;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(path: "Logs/log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddCors( 
    options => { options
        .AddDefaultPolicy(policy =>  policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    }
);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<GlobalErrorHandler>();

//DTOs Mapping
builder.Services.AddAutoMapper(typeof(MappingProfiles));

//Add Scopes
builder.Services.AddScoped<IJWT, JWTRepository>();
builder.Services.AddScoped<IAuth, AuthRepository>();
builder.Services.AddScoped<IRole, RoleRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IJob, JobRepository>();
builder.Services.AddScoped<IJobPosition, JobPositionRepository>();
builder.Services.AddScoped<IJobType, JobTypeRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<ICompany, CompanyRepository>();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;   
})
    .AddJwtBearer(options =>
    {    
        options.TokenValidationParameters = new TokenValidationParameters
        {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        //ValidIssuer = builder.Configuration["JWT:Issuer"],
        //ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalErrorHandler>();
app.UseHttpsRedirection();
app.UseCors();
//app.UseCors("CustomPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
