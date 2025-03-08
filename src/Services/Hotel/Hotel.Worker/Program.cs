using Hotel.Infrastructure;
using Hotel.Worker;
using NextDestiny.Core.Amqp;
using NextDestiny.Core.SecretManager;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddSecretManager();
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddAmqpServices(builder.Configuration, typeof(HotelWorker));

var host = builder.Build();
host.Run();
