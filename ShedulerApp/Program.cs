using Microsoft.EntityFrameworkCore;
using ShedulerApp.Models;
using Hangfire;
using Hangfire.SQLite;
using ShedulerApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CustomDbContext>(options=>options.UseSqlite(@"Data Source=Sheduler.db"));
builder.Services.AddCors();
var authConfig = builder.Configuration.GetSection("Auth");
builder.Services.Configure<AuthOptions>(authConfig);
var authOptions = authConfig.Get<AuthOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = authOptions.Issuer,

        ValidateAudience = true,
        ValidAudience = authOptions.Audience,

        ValidateLifetime = true,

        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true,
    };
});


builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ITaskDbHandler, TaskDbHandler>();
builder.Services.AddTransient<IApiRequest, ApiRequest>();
var sqliteOptions = new SQLiteStorageOptions();
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage(@"Data Source=Sheduler.db;", sqliteOptions)
);
builder.Services.AddHangfireServer(options => options.WorkerCount = 1);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();
app.UseCors(options =>
options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();
