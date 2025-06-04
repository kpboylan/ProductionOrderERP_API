using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using ProductionOrderERP_API.ERP.Core.Service;
using ProductionOrderERP_API.ERP.Application.Service;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Infrastructure.Repository;
using ProductionOrderERP_API.ERP.Core.Helper;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;
using ProductionOrderERP_API.ERP.Application.UseCase;
using ProductionOrderERP_API.ERP.Infrastructure.Service;
using ProductionOrderERP_API.ERP.Application.UseCase.Room.ML;
using ProductionOrderERP_API.ERP.Application.UseCase.Room;
using Polly.Wrap;
using ProductionOrderERP_API.ERP.Application.Polly;
using ProductionOrderERP_API.ERP.Core.Entity;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

var builder = WebApplication.CreateBuilder(args);

var logFileName = $"logs/log-{DateTime.Now:MM-dd-yyyy}.txt";


builder.Services.AddControllers()
    .AddNewtonsoftJson();

var connectionString = builder.Configuration["AppConfigConnectionString"];

builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(connectionString)
           .UseFeatureFlags();
});

builder.Services.AddScoped<IProdOrderService, ProdOrderService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductServiceHelper, ProductServiceHelper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IRoomRepository, SqlServerRoomRepository>();
builder.Services.AddScoped<IGetRoomTempUseCase, GetRoomTempUseCase>();
builder.Services.AddScoped<IGetRoomHumidityUseCase, GetRoomHumidityUseCase>();
builder.Services.AddScoped<IPendingMessageRepository, PendingMessageRepository>();
builder.Services.AddScoped<IFeatureFlagService, FeatureFlagService>();
builder.Services.AddScoped(typeof(IGenericRabbitMQService<>), typeof(GenericRabbitMQService<>));
builder.Services.AddScoped<MessageBus>();
builder.Services.AddScoped<CreateMaterialUseCase>();
builder.Services.AddScoped<GetMaterialsUseCase>();
builder.Services.AddScoped<GetActiveMaterialsUseCase>();
builder.Services.AddScoped<GetMaterialUseCase>();
builder.Services.AddScoped<UpdateMaterialUseCase>();
builder.Services.AddScoped<GetMaterialTypesUseCase>();
builder.Services.AddScoped<GetUOMsUseCase>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<GetUsersUseCase>();
builder.Services.AddScoped<GetActiveUsersUseCase>();
builder.Services.AddScoped<GetUserUseCase>();
builder.Services.AddScoped<UpdateUserUseCase>();
builder.Services.AddScoped<ValidateUserUseCase>();
builder.Services.AddScoped<GetUserTypesUseCase>();
builder.Services.AddScoped<PublishCreateMaterialUseCase>();
builder.Services.AddScoped<PublishUpdateMaterialUseCase>();
builder.Services.AddScoped<SaveChangesUseCase>();
builder.Services.AddScoped<ProductExistsUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<GetActiveProductsUseCase>();


builder.Services.AddScoped<GetAllProductsUseCase>();
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<ValidateUserUseCase>();
builder.Services.AddScoped<GenerateTokenUseCase>();
builder.Services.AddScoped<TempAnomalyDetection>();
builder.Services.AddScoped<HumiditySpikeDetection>();
builder.Services.AddScoped<PendingQueueMessage>();
builder.Services.AddScoped<FeatureFlagRepository>();
builder.Services.AddScoped<TenantRepository>();

builder.Services.AddSingleton<IRabbitMqResiliencePolicyProvider, RabbitMqResiliencePolicyProvider>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ITargetingContextAccessor, TenantTargetingContextAccessor>();

builder.Services.AddFeatureManagement()
    .AddFeatureFilter<TargetingFilter>();



builder.Services.AddSingleton<AsyncPolicyWrap>(provider =>
{
    var fallbackAction = () => Task.CompletedTask; // Replace with actual injected logic if needed
    return PollyPolicies.CreateRabbitMqResiliencePolicy(fallbackAction);
});

if (builder.Configuration["DbSettings:DbProvider"] == "PostgresSQL")
{
    builder.Services.AddScoped<IRoomRepository, PostgresRoomRepository>();
    builder.Services.AddDbContext<ERPContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
}
else
{
    builder.Services.AddScoped<IRoomRepository, SqlServerRoomRepository>();
    builder.Services.AddDbContext<ERPContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
}

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProductionOrderERP_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(logFileName, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisaverylongsecretkeythatis256bits"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Debug: Print all configuration keys (filter for Azure App Config)
var config = app.Services.GetRequiredService<IConfiguration>();
Console.WriteLine("===== Configuration Keys =====");
foreach (var entry in config.AsEnumerable())
{
    if (entry.Key.StartsWith("FeatureManagement:") || entry.Key.Contains("AzureAppConfig"))
        Console.WriteLine($"{entry.Key} = {entry.Value}");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  // ✅ Must be before UseAuthorization
app.UseAuthorization();   // ✅ Only once

app.MapControllers();

app.Run();
