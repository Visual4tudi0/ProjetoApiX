using ClassLibrary1Service.Service;
using ClassLibrary1Service.Validation;
using ClassLibraryData1.Context;
using ClassLibraryData1.Mapping;
using ClassLibraryData1.Repository;
using ClassLibraryData1.Rest;
using ClassLibraryDomain.IRepository;
using ClassLibraryDomain.IRestRepository;
using ClassLibraryDomain.IService;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// 1. Configura��o do DbContext (Entity Framework Core)
// Pega a string de conex�o do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Configura��o da Inje��o de Depend�ncias para Reposit�rios
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IRepositoryProduto, RepositoryProduto>();
builder.Services.AddScoped<IRepositoryCategoria, RepositoryCategoria>();

// 3. Configura��o da Inje��o de Depend�ncias para UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 4. Configura��o da Inje��o de Depend�ncias para Servi�os da Camada Application
builder.Services.AddScoped<IServiceProduto, ServiceProduto>();
builder.Services.AddScoped<IServiceCategoria, CategoriaService>();

// 5. Configura��o da Inje��o de Depend�ncias para Servi�os REST (IRestCotacaoDolar)
// Esta parte assume que IRestCotacaoDolar est� no dom�nio e sua implementa��o em SistemaVendas.Data.Rest
// � recomend�vel configurar o HttpClientFactory para chamadas a APIs externas
builder.Services.AddHttpClient<IRestRepositoryDolar, RestRepositoryDolar>(); // 'RestCotacaoDolar' � a implementa��o concreta

// 6. Configura��o do AutoMapper
// Ele buscar� os perfis de mapeamento em todos os assemblies referenciados
// O ideal � colocar os perfis em uma camada espec�fica, ex: SistemaVendas.Application/Mappers/MappingProfile.cs
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Ou, para maior controle, especifique os assemblies que cont�m seus perfis:
// builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); // Assumindo MappingProfile em SistemaVendas.Application

// 7. Configura��o do FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        // Registra os validadores em todos os assemblies referenciados.
        // � uma boa pr�tica ter validadores em uma camada de Application ou Domain,
        // mas o controlador aqui ir� descobrir.
        // O ideal � especificar o assembly onde est�o os validadores, ex:
        // fv.RegisterValidatorsFromAssembly(typeof(ProdutoModelValidator).Assembly);
        fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        // Opcional: Desabilita a valida��o de modelo padr�o do .NET para confiar apenas no FluentValidation
        // fv.DisableDataAnnotationsValidation = true;
    });

// Adiciona servi�os aos containers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configura��o do pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
