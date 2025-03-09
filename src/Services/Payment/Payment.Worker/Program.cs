using NextDestiny.Core.Amqp;
using NextDestiny.Core.SecretManager;
using Payment.Application.Interfaces;
using Payment.Application.Services;
using Payment.Worker.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddSecretManager();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddAmqpServices(builder.Configuration, typeof(PaymentRequestConsumer));

var host = builder.Build();
host.Run();
