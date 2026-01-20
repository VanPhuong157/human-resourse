using BusinessObjects.Files;
using BusinessObjects.Models;
using DataAccess.Emails;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Departments;
using Repository.Notifications;
using Repository.Objectives;
using Repository.OkrHistories;
using Repository.Permissions;
using Repository.PolicyRepository;
using Repository.Roles;
using Repository.Schedules;
using Repository.Statistic;
using Repository.UserGroups;
using Repository.UserHistories;
using Repository.UserInformations;
using Repository.Users;
using Repository.WorkFlows;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new DateTimeConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHangfireServer();
builder.Services.AddHttpClient();

builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("SEP490_Worker")));
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// ✅ Cấu hình CORS
var allowedOrigins = new[]
{
    "http://27.71.26.109",
    "http://sep490g49-ui.eastasia.cloudapp.azure.com",
    "http://localhost:3000"
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", policy =>
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithExposedHeaders("Content-Disposition", "filename")
    );
});

// Repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserInformationRepository, UserInformationRepository>();
builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IUserHistoryRepository, UserHistoryRepository>();
builder.Services.AddTransient<IOkrRepository, OkrRepository>();
builder.Services.AddTransient<IOkrHistoryRepository, OkrHistoryRepository>();
builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
builder.Services.AddTransient<IUserGroupRepository, UserGroupRepository>();
builder.Services.AddTransient<EmailDAO, EmailDAO>();
builder.Services.AddScoped<IPolicyStepRepository, PolicyStepRepository>();
builder.Services.AddScoped<IWorkFlowRepository, WorkFlowRepository>();
builder.Services.AddSingleton<IFileStorage, FileStorage>();
builder.Services.AddTransient<IScheduleRepository, ScheduleRepository>();

builder.Services.AddDbContext<SEP490_G49Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SEP490_G49"));
});

builder.Services.AddAuthentication(options =>
{                                               
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

builder.WebHost.UseUrls("http://*:8080");

var uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "Uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

// ✅ Đặt UseCors đúng vị trí
app.UseCors("DefaultCors");

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/dashboard");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/Uploads"
});

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notificationHub");
    // Other endpoints...
});

// Tự động Migrate Database khi khởi chạy
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SEP490_G49Context>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Lỗi xảy ra khi tự động cập nhật Database.");
    }
}

app.Run();
