using FluentValidation;
using Tn.Inventory.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var dbConnection = builder.Configuration.GetConnectionString("DbConnection");
ArgumentException.ThrowIfNullOrEmpty(dbConnection);

builder.Services.AddInventoryDbContext(dbConnection, ServiceLifetime.Scoped);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddCors();

builder.Services.AddValidatorsFromAssembly(Tn.Inventory.Application.AssemblyReference.Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Tn.Inventory.Application.AssemblyReference.Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Tn.Inventory.Domain.AssemblyReference.Assembly));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.Run();