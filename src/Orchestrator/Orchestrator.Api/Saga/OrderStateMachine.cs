using MassTransit;
using NextDestiny.Core.Shared.Events.Flight;
using NextDestiny.Core.Shared.Events.Hotel;
using NextDestiny.Core.Shared.Events.Order;
using NextDestiny.Core.Shared.Events.Payment;
using Orchestrator.Api.Service;

namespace Orchestrator.Api.Saga
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public State Submitted { get; private set; } = default!;
        public State FlightBooked { get; private set; } = default!;
        public State HotelBooked { get; private set; } = default!;
        public State PaymentProcessed { get; private set; } = default!;
        public State Completed { get; private set; } = default!;
        public State Failed { get; private set; } = default!;

        public Event<OrderSubmitted> OrderSubmitted { get; private set; } = default!;
        public Event<FlightBookingCompleted> FlightBookingCompleted { get; private set; } = default!;
        public Event<FlightBookingFailed> FlightBookingFailed { get; private set; } = default!;
        public Event<HotelBookingCompleted> HotelBookingCompleted { get; private set; } = default!;
        public Event<HotelBookingFailed> HotelBookingFailed { get; private set; } = default!;
        public Event<PaymentProcessed> PaymentSucces { get; private set; } = default!;
        public Event<PaymentFailed> PaymentFailed { get; private set; } = default!;

        // Compensation
        public Event<FlightBookingCancelled> FlightBookingCancelled { get; private set; } = default!;
        public Event<HotelBookingCancelled> HotelBookingCancelled { get; private set; } = default!;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OrderStateMachine(IServiceScopeFactory serviceScopeFactory)
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderSubmitted, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => FlightBookingCompleted, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => FlightBookingFailed, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => HotelBookingCompleted, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => HotelBookingFailed, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => PaymentSucces, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => PaymentFailed, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => FlightBookingCancelled, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => HotelBookingCancelled, x => x.CorrelateById(context => context.Message.OrderId));

            Initially(
                When(OrderSubmitted)
                      .Then(context =>
                      {
                          context.Saga.OrderId = context.Message.OrderId;
                          Console.WriteLine($"Pedido enviado");

                          TrackingMessage(context.Message.OrderId, "Pedido enviado");
                      })
                    .TransitionTo(Submitted)
                    .Publish(context => new FlightBookingRequested
                    {
                        OrderId = context.Message.OrderId,
                        CustomerEmail = context.Message.CustomerEmail
                    })
            );
            //// Reserva de voo bem-sucedida
            During(Submitted,
                When(FlightBookingCompleted)
                    .Then(context =>
                    {
                        TrackingMessage(context.Message.OrderId, "Reserva de voo bem-sucedida");
                    })
                    .TransitionTo(FlightBooked)
                    .Publish(context => new HotelBookingRequested(context.Saga.OrderId)),

                When(FlightBookingFailed)
                    //.Then(context => context.Saga.Timestamp = context.Message.Timestamp)
                    .TransitionTo(Failed)
                    .Publish(context => new OrderFailed(context.Saga.OrderId, "Falha na reserva do voo"))
            );

            //// Reserva de hotel bem-sucedida
            During(FlightBooked,
                When(HotelBookingCompleted)
                    .Then(context =>
                    {
                        TrackingMessage(context.Message.OrderId, "Reserva de hotel bem-sucedida");
                    })
                    .TransitionTo(HotelBooked)
                    .Publish(context => new PaymentRequest(context.Saga.OrderId)),

                When(HotelBookingFailed)
                    .Then(context =>
                    {
                        TrackingMessage(context.Message.OrderId, "Falha na reserva do hotel");
                    })
                    .TransitionTo(Failed)
                    .Publish(context => new FlightBookingCancellationRequested(context.Saga.OrderId)) // Rollback da reserva do voo
            );

            //// Pagamento processado com sucesso
            During(HotelBooked,
                When(PaymentSucces)
                    .Then(context =>
                    {
                        TrackingMessage(context.Message.OrderId, "Pagamento processado com sucesso");
                    })
                    .TransitionTo(Completed)
                    .Publish(context => new OrderCompleted(context.Saga.OrderId)),

                When(PaymentFailed)
                    .Then(context =>
                    {
                        TrackingMessage(context.Message.OrderId, "Falha no pagamento");
                    })
                    .TransitionTo(Failed)
                    .Publish(context => new HotelBookingCancellationRequested(context.Saga.OrderId)) // Rollback do hotel
                    .Publish(context => new FlightBookingCancellationRequested(context.Saga.OrderId)) // Rollback do voo
            );

            //// Compensação (Rollback)
            During(Failed,
                When(FlightBookingCancelled)
                    .Then(context => Console.WriteLine($"Reserva de voo cancelada para o pedido {context.Message.OrderId}")),

                When(HotelBookingCancelled)
                    .Then(context => Console.WriteLine($"Reserva de hotel cancelada para o pedido {context.Message.OrderId}"))
            );

            _serviceScopeFactory = serviceScopeFactory;
        }

        private void TrackingMessage(Guid orderId, string message)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _logger = scope.ServiceProvider.GetRequiredService<ILogger<OrderStateMachine>>();
                _logger.LogInformation($"Pedido {orderId}: {message}");

                var trackingService = scope.ServiceProvider.GetRequiredService<ITrackingService>();

                trackingService.SendTrackingEventAsync(orderId, message);
            }
        }

    }



    public class OrderState : SagaStateMachineInstance
        {
            public Guid CorrelationId { get; set; }
            public string CurrentState { get; set; }
            public Guid OrderId { get; set; }
            public DateTime Timestamp { get; set; }
        }
    
}
