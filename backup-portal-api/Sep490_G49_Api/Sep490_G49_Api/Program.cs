using BusinessObjects.Models;
using DataAccess.Departments;
using DataAccess.Candidates;
using DataAccess.JobPosts;
using DataAccess.Okrs;
using DataAccess.Roles;
using DataAccess.UserHistories;
using DataAccess.UserInformations;
using DataAccess.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Departments;
using Repository.Candidates;
using Repository.JobPosts;
using Repository.Roles;
using Repository.UserHistories;
using Repository.UserInformations;
using Repository.Users;
using System.Text;
using Repository.Objectives;
using DataAccess.OkrHistories;
using Repository.OkrHistories;
using DataAccess.Emails;
using DataAccess.NotificationsDAO;
using Repository.Notifications;
using Repository.Statistic;
using Repository.HomePages;
using DataAccess.HomePages;
using Repository.Permissions;
using DataAccess.Permissions;
using DataAccess.HomePageReasons;
using Repository.HomePageReasons;
using Repository.UserGroups;
using DataAccess.UserGroups;
using Microsoft.Extensions.FileProviders;
using Hangfire;
using BusinessObjects.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new DateTimeConverter());
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
        options.AddDefaultPolicy(policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod()
    ));
}
else
{
    builder.Services.AddCors(options =>
        options.AddDefaultPolicy(policy =>
            policy.WithOrigins("http://sep490g49-ui.eastasia.cloudapp.azure.com")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials()
    ));
}
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IJobPostRepository, JobPostRepository>();
builder.Services.AddTransient<IUserInformationRepository, UserInformationRepository>();
builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddTransient<ICandidateRepository, CandidateRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IUserHistoryRepository, UserHistoryRepository>();
builder.Services.AddTransient<IOkrRepository, OkrRepository>();
builder.Services.AddTransient<IOkrHistoryRepository, OkrHistoryRepository>();
builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
builder.Services.AddTransient<IHomePageRepository, HomePageRepository>();
builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
builder.Services.AddTransient<IHomePageReasonRepository, HomePageReasonRepository>();
builder.Services.AddTransient<IUserGroupRepository, UserGroupRepository>();
builder.Services.AddTransient<UserDAO, UserDAO>();
builder.Services.AddTransient<OkrDAO, OkrDAO>();
builder.Services.AddTransient<OkrHistoryDAO, OkrHistoryDAO>();
builder.Services.AddTransient<JobPostDAO, JobPostDAO>();
builder.Services.AddTransient<UserInformationDAO, UserInformationDAO>();
builder.Services.AddTransient<DepartmentDAO, DepartmentDAO>();
builder.Services.AddTransient<RoleDAO, RoleDAO>();
builder.Services.AddTransient<CandidateDAO, CandidateDAO>();
builder.Services.AddTransient<EmailDAO, EmailDAO>();
builder.Services.AddTransient<UserHistoryDAO, UserHistoryDAO>();
builder.Services.AddTransient<NotificationDAO, NotificationDAO>();
builder.Services.AddTransient<HomePageDAO, HomePageDAO>();
builder.Services.AddTransient<PermissionDAO, PermissionDAO>();
builder.Services.AddTransient<HomePageReasonDAO, HomePageReasonDAO>();
builder.Services.AddTransient<UserGroupDAO, UserGroupDAO>();


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
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
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
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
var uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "Uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseHangfireDashboard("/dashboard");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
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
app.Run();