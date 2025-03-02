
using Catalog.Infrastructure;
using NextDestiny.Core.SecretManager;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddSecretManager("next-destiny-sc");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfraCatalog(builder.Configuration);

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
