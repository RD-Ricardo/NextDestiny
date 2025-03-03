using Hotel.Infrastructure;
using Hotel.Worker;
using NextDestiny.Core.Amqp;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddAmqpServices(builder.Configuration, typeof(HotelWorker));

var host = builder.Build();
host.Run();
