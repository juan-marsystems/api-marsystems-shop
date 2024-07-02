using ClassLibrary1.models;
using ClassLibrary1.Repositories;
using Microsoft.EntityFrameworkCore;
using ClassLibrary1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MarketSystemsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MarketSystemsConnection")));
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().AllowAnyOrigin().WithOrigins("http://localhost:4200");
}));
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>(); 
builder.Services.AddScoped<IArticulosRepository, ArticulosRepository>();

var app = builder.Build();
app.UseCors("corsapp");

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
