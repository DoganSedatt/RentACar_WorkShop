using Business.DependencyResolvers;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//BasmaKalıp kodlar
//-----------------------------
TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true, // Issuer'ı validate etmeli mi?
            ValidateAudience = true,// Audience'ı validate etmeli mi?
            ValidateLifetime = true, // Süreyi validate etmeli mi?
            ValidateIssuerSigningKey = true, // Security key validate etmeli mi?
            

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)), // Valid security key değeri
            ValidIssuer = tokenOptions.Issuer,// Valid Issuer değeri
            ValidAudience = tokenOptions.Audience,// Valid Audience değeri
            
            
            
        };
    });

//Buraya kadar
//--------------------------------
var app = builder.Build();

//if (app.Environment.IsProduction())
    app.UseGlobalExceptionHandling();

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
