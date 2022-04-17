using CDA.Data;
using CDA.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using Swashbuckle.Swagger;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthenticationService, AutheticationService>();
var authkey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Key"];
var key = Encoding.ASCII.GetBytes(authkey);
builder.Services.AddAuthentication(x =>
{

    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.AddSecurityDefinition(
        "token",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization
        }
    );
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "token"
                    },
                },
                Array.Empty<string>()
            }
        }
    );
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = "Exemplo de USO da API, \n\nCrie um usuario enviando um 'POST' request no endpoint /api/Users. \n\nFa�a Login enviando 'POST' request no endpoint /api/Auth/login" +
        " inserindo o usuario e senha que acabou de criar anteriormente.\n Caso haja sucesso irá retornar um Token, copie ele vá na Aba 'Authorize' do Swagger,\n\n Insira o Token. Agora todas as outras " +
        "rotas estão liberadas!\n\n Comece criando um 'Status' pois Criminal code necessita de um Status criado com seu Id.",
    });
});
builder.Services.AddDbContext<Context>(options=> options.UseMySql("server=db;initial catalog=CDA;uid=SEU_USUARIO;pwd=SUA_SENHA", 
       Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.37-mysql")));


var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
