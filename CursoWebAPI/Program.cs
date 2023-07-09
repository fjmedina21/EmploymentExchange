using EmploymentExchange;
using EmploymentExchange.Data;
using EmploymentExchange.Repositories;
using EmploymentExchange.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using RH = System.Text.Json.Serialization.ReferenceHandler;
using E = System.Text.Encoding;
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
    options => 
    { 
        options.AddDefaultPolicy( 
            policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        /*options.AddPolicy( "CustomPolicy",
            policy => { policy.WithOrigins("http://example.com", "http://www.contoso.com")
                           .WithHeaders(HeaderNames.ContentType, "auth")
                           .WithMethods("POST", "PUT", "DELETE", "GET");
            });*/
    }
);

// Add services to the container.
builder.Services.AddResponseCaching();
builder.Services.AddControllers(option =>
{
    option.CacheProfiles.Add("Default", 
        new CacheProfile()
        {
            Duration = 60*2,
            Location = ResponseCacheLocation.Client
        });
}).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = RH.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
});

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
}
)
    .AddJwtBearer(options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        //ValidIssuers = builder.Configuration["JWT:Issuers"]
        //ValidAudiences = builder.Configuration["JWT:Audiences"]
        IssuerSigningKey = new SymmetricSecurityKey(
            E.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
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
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
