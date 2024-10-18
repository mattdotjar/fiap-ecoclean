using EcoClean.Data.Contexts;
using EcoClean.Data.Repository;
using EcoClean.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001);
});

// Adicionar controladores (corrige o erro atual)
builder.Services.AddControllers();

// Conexão com o banco de dados Oracle
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);

// Registro de serviços e repositórios
builder.Services.AddScoped<IColetaRepository, ColetaRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<INotificacaoRepository, NotificacaoRepository>();
builder.Services.AddScoped<IMoradorRepository, MoradorRepository>();
builder.Services.AddScoped<ICaminhaoRepository, CaminhaoRepository>();
builder.Services.AddScoped<IRotaRepository, RotaRepository>();
builder.Services.AddScoped<ITipoResiduoRepository, TipoResiduoRepository>();

builder.Services.AddScoped<IColetaService, ColetaService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();
builder.Services.AddScoped<IMoradorService, MoradorService>();
builder.Services.AddScoped<ICaminhaoService, CaminhaoService>();
builder.Services.AddScoped<IRotaService, RotaService>();
builder.Services.AddScoped<ITipoResiduoService, TipoResiduoService>();

// Adicionar TokenService
builder.Services.AddScoped<TokenService>();


// Adicionar serviços de Autenticação e Autorização
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "ecoclean",
        ValidateAudience = true,
        ValidAudience = "your-api",
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi")
        ),
        ValidateIssuerSigningKey = true
    };
});


// Adicionar Autorização
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Mapeamento de controladores (corrige o erro atual)
app.MapControllers();

app.Run();
