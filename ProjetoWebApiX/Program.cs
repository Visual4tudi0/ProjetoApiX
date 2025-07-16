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

// 1. Configuração do DbContext (Entity Framework Core)
// Pega a string de conexão do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Configuração da Injeção de Dependências para Repositórios
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IRepositoryProduto, RepositoryProduto>();
builder.Services.AddScoped<IRepositoryCategoria, RepositoryCategoria>();

// 3. Configuração da Injeção de Dependências para UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 4. Configuração da Injeção de Dependências para Serviços da Camada Application
builder.Services.AddScoped<IServiceProduto, ServiceProduto>();
builder.Services.AddScoped<IServiceCategoria, CategoriaService>();

// 5. Configuração da Injeção de Dependências para Serviços REST (IRestCotacaoDolar)
// Esta parte assume que IRestCotacaoDolar está no domínio e sua implementação em SistemaVendas.Data.Rest
// É recomendável configurar o HttpClientFactory para chamadas a APIs externas
builder.Services.AddHttpClient<IRestRepositoryDolar, RestRepositoryDolar>(); // 'RestCotacaoDolar' é a implementação concreta

// 6. Configuração do AutoMapper
// Ele buscará os perfis de mapeamento em todos os assemblies referenciados
// O ideal é colocar os perfis em uma camada específica, ex: SistemaVendas.Application/Mappers/MappingProfile.cs
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Ou, para maior controle, especifique os assemblies que contêm seus perfis:
// builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); // Assumindo MappingProfile em SistemaVendas.Application

// 7. Configuração do FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        // Registra os validadores em todos os assemblies referenciados.
        // É uma boa prática ter validadores em uma camada de Application ou Domain,
        // mas o controlador aqui irá descobrir.
        // O ideal é especificar o assembly onde estão os validadores, ex:
        // fv.RegisterValidatorsFromAssembly(typeof(ProdutoModelValidator).Assembly);
        fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        // Opcional: Desabilita a validação de modelo padrão do .NET para confiar apenas no FluentValidation
        // fv.DisableDataAnnotationsValidation = true;
    });

// Adiciona serviços aos containers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
