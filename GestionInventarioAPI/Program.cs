using GestionInventarioAPI.Data;
using GestionInventarioAPI.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<AlmacenRepositorio, AlmacenRepositorio>();
builder.Services.AddScoped<ArticuloRepositorio, ArticuloRepositorio>();
builder.Services.AddScoped<CategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<SubCategoriaRepositorio, SubCategoriaRepositorio>();
builder.Services.AddScoped<CompraRepositorio, CompraRepositorio>();
builder.Services.AddScoped<ProveedoresRepositorio, ProveedoresRepositorio>();
builder.Services.AddScoped<OrdenPedidoRepositorio, OrdenPedidoRepositorio>();
builder.Services.AddScoped<DetalleOrdenPedidoRepositorio, DetalleOrdenPedidoRepositorio>();
builder.Services.AddScoped<SalidasAlmacenRepositorio, SalidasAlmacenRepositorio>();
builder.Services.AddScoped<DetalleSalidaRepositorio, DetalleSalidaRepositorio>(); 
builder.Services.AddScoped<DetalleCompraRepositorio, DetalleCompraRepositorio>();
builder.Services.AddScoped<NotaCreditoRepositorio, NotaCreditoRepositorio>();
builder.Services.AddScoped<DetalleNotaCreditoRepositorio, DetalleNotaCreditoRepositorio>();
builder.Services.AddScoped<UsuariosRepositorio, UsuariosRepositorio>();














builder.Services.AddDbContext<AppDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
