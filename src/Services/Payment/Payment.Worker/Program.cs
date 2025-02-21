using NextDestiny.Core.Amqp;
using Payment.Worker.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAmqpServices(builder.Configuration, typeof(PaymentRequestConsumer));

var host = builder.Build();
host.Run();
