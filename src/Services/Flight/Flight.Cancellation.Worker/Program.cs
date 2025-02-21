using Flight.Cancellation.Worker;
using NextDestiny.Core.Amqp;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddAmqpServices(builder.Configuration, typeof(Worker));
var host = builder.Build();
host.Run();
