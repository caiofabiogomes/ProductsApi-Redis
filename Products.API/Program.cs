using Products.API.Middlewares;
using Products.Application;
using Products.Infrastructure;
using Products.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<GlobalExceptionHandler>();
// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandler>();

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    DbInitializer.Initialize(serviceProvider);
}

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
