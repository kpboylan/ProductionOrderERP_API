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

var builder = WebApplication.CreateBuilder(args);

var logFileName = $"logs/log-{DateTime.Now:MM-dd-yyyy}.txt";


builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddTransient<IProdOrderService, ProdOrderService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductServiceHelper, ProductServiceHelper>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IMaterialRepository, MaterialRepository>();
builder.Services.AddTransient(typeof(IGenericRabbitMQService<>), typeof(GenericRabbitMQService<>));
builder.Services.AddTransient<CreateMaterialUseCase>();
builder.Services.AddTransient<GetMaterialsUseCase>();
builder.Services.AddTransient<GetMaterialUseCase>();
builder.Services.AddTransient<UpdateMaterialUseCase>();
builder.Services.AddTransient<GetMaterialTypesUseCase>();
builder.Services.AddTransient<GetUOMsUseCase>();
builder.Services.AddTransient<CreateUserUseCase>();
builder.Services.AddTransient<GetUsersUseCase>();
builder.Services.AddTransient<GetUserUseCase>();
builder.Services.AddTransient<UpdateUserUseCase>();
builder.Services.AddTransient<ValidateUserUseCase>();
builder.Services.AddTransient<GetUserTypesUseCase>();
builder.Services.AddTransient<PublishCreateMaterialUseCase>();
builder.Services.AddTransient<PublishUpdateMaterialUseCase>();
builder.Services.AddTransient<SaveChangesUseCase>();
builder.Services.AddTransient<ProductExistsUseCase>();
builder.Services.AddTransient<UpdateProductUseCase>();
builder.Services.AddTransient<GetProductByIdUseCase>();
builder.Services.AddTransient<GetActiveProductsUseCase>();
builder.Services.AddTransient<GetAllProductsUseCase>();
builder.Services.AddTransient<CreateProductUseCase>();
builder.Services.AddTransient<ValidateUserUseCase>();
builder.Services.AddTransient<GenerateTokenUseCase>();

builder.Services.AddDbContext<ERPContext>(
    dbContextOptions => dbContextOptions.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Production_ERP"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
