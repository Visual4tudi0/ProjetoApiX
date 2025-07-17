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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IRepositoryProduto, RepositoryProduto>();
builder.Services.AddScoped<IRepositoryCategoria, RepositoryCategoria>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IServiceProduto, ServiceProduto>();
builder.Services.AddScoped<IServiceCategoria, CategoriaService>();

builder.Services.AddHttpClient<IRestRepositoryDolar, RestRepositoryDolar>(); 

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
