using GestionInventarioAPI.Data;
using GestionInventarioAPI.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
builder.Services.AddScoped<LoginRepositorio, LoginRepositorio>();


var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

//Aqu� se configura la Autenticaci�n JSON Web Token - Primera parte
builder.Services.AddAuthentication(x =>
{
    // Establece el esquema de autenticaci�n por defecto para la autenticaci�n
    // basada en JWT como el esquema de autenticaci�n por defecto.
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    // Establece el esquema de desaf�o por defecto para la autenticaci�n
    // basada en JWT como el esquema de desaf�o por defecto.
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    // Agrega la autenticaci�n JWT Bearer al servicio de autenticaci�n.
}).AddJwtBearer(x =>
{
    // Indica si se debe validar el origen HTTPS del token.
    x.RequireHttpsMetadata = false;
    // Indica si el token recibido debe ser almacenado.
    x.SaveToken = true;

    // Especifica los par�metros para la validaci�n del token.
    x.TokenValidationParameters = new TokenValidationParameters
    {
        // Indica si se debe validar la clave de firma del emisor del token.
        ValidateIssuerSigningKey = true,
        // Establece la clave de firma usada para validar los tokens recibidos.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        // Indica si se debe validar el emisor del token.
        ValidateIssuer = false,
        // Indica si se debe validar el destinatario del token.
        ValidateAudience = false
    };
});
















builder.Services.AddDbContext<AppDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//Aqu� se configura la autenticaci�n y autorizaci�n - segunda parte
builder.Services.AddSwaggerGen(options =>
{
    // Agrega una definici�n de seguridad para el esquema Bearer
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        // Descripci�n del esquema de seguridad Bearer
        Description =
        "Autenticaci�n JWT usando el esquema Bearer. \r\n\r\n " +
        "Ingresa la palabra 'Bearer' seguida de un [espacio] y despues su token en el campo de abajo \r\n\r\n" +
        "Ejemplo: \"Bearer tkdknkdllskd\"",
        // Nombre del esquema de seguridad en la solicitud de autorizaci�n
        Name = "Authorization",
        // Ubicaci�n del token en la solicitud (en el encabezado)
        In = ParameterLocation.Header,
        // Tipo de esquema de seguridad (Bearer)
        Scheme = "Bearer"
    });
    // Agrega un requerimiento de seguridad para el esquema Bearer
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
			// Especifica el esquema de seguridad que se debe cumplir
			new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                            {
								// Tipo de referencia (esquema de seguridad)
								Type = ReferenceType.SecurityScheme,
								// Identificador del esquema de seguridad (Bearer)
								Id = "Bearer"
                            },
				// Especifica el tipo de esquema de seguridad (oauth2)
				Scheme = "oauth2",
				// Nombre del esquema de seguridad en la solicitud de autorizaci�n
				Name = "Bearer",
	 // Ubicaci�n del token en la solicitud (en el encabezado)
				In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});



builder.Services.AddCors(P => P.AddPolicy("PolicyCors", buid =>
{

    //Aqui dentro es donde va la lista de dominios aceptados
    //Si yo ese s�mbolo lo quito y quiero establecer x dominios ah� adentro debo de poner el nombre los dominios que quiero que accedan a la Api, entonces de esa manera yo restrinjo

    //Y ahora esa pol�tica PolicyCors tenemos que inyectarla en el servidor 

    buid.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}

));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PolicyCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
