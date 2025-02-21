using MassTransit;
using NextDestiny.Core.Shared.Events.Flight;
using NextDestiny.Core.Shared.Events.Hotel;
using NextDestiny.Core.Shared.Events.Order;
using NextDestiny.Core.Shared.Events.Payment;

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

        public OrderStateMachine()
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
                          Console.WriteLine($"Teste inicializar");
                      })
                    .TransitionTo(Submitted)
                    .Publish(context => new FlightBookingRequested(context.Saga.OrderId))
            );
            //// Reserva de voo bem-sucedida
            During(Submitted,
                When(FlightBookingCompleted)
                    .Then(context =>
                    {
                        Console.WriteLine("Reserva de voo bem-sucedida");
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
                    .Then(context => context.Saga.Timestamp = context.Message.Timestamp)
                    .TransitionTo(HotelBooked)
                    .Publish(context => new PaymentRequest(context.Saga.OrderId)),

                When(HotelBookingFailed)
                    .Then(context => context.Saga.Timestamp = context.Message.Timestamp)
                    .TransitionTo(Failed)
                    .Publish(context => new FlightBookingCancellationRequested(context.Saga.OrderId)) // Rollback da reserva do voo
            );

            //// Pagamento processado com sucesso
            During(HotelBooked,
                When(PaymentSucces)
                    .Then(context => context.Saga.Timestamp = context.Message.Timestamp)
                    .TransitionTo(Completed)
                    .Publish(context => new OrderCompleted(context.Saga.OrderId)),

                When(PaymentFailed)
                    .Then(context => context.Saga.Timestamp = context.Message.Timestamp)
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
