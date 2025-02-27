using System;
using Flight.Domain.Entities;
using Flight.Domain.Enums;
using Flight.Domain.Repositories;
using NextDestiny.Core.Amqp.Abstractions;
using NextDestiny.Core.Shared.Events.Flight;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Flight.Application.Services
{
    public class FlightService : IFlightService
    {
        private readonly IBusService _busService;

        private readonly IFlightBookingRepository _flightBookingRepository;

        public FlightService(IBusService busService, IFlightBookingRepository flightBookingRepository)
        {
            _busService = busService;
            _flightBookingRepository = flightBookingRepository;
        }

        public async Task BookingAsync(FlightBookingRequested flightBookingRequested)
        {
            var origin = GetOrigin();

            var flightBooking = new FlightBooking
            {
                OrderId = flightBookingRequested.OrderId,
                PassengerEmail = flightBookingRequested.CustomerEmail,
                Date = GenerateDate(),
                Hour = GenerateHour(),
                Origin = origin,
                Destination = GetDestination(origin),
                Status = FlightBookingStatus.Pending,
                Events =
                [
                    new()
                    {
                        Name = "Reserva de voo criada",
                        CreatedAt = DateTime.Now
                    }
                ],
            };

            await _flightBookingRepository.CreateAsync(flightBooking);

            // Simulate booking of flight
            flightBooking = TryBooking(flightBooking);

            await _flightBookingRepository.UpdateAsync(flightBooking);

            if (flightBooking.Status == FlightBookingStatus.Confirmed)
            {
                await _busService.PublishAsync(new FlightBookingCompleted
                {
                    OrderId = flightBooking.OrderId,
                    Timestamp = DateTime.UtcNow
                });
            }
            else if (flightBooking.Status == FlightBookingStatus.Canceled)
            {
                await _busService.PublishAsync(new FlightBookingFailed
                {
                    OrderId = flightBooking.OrderId,
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        public async Task CancelAsync(FlightBookingCancellationRequested flightBookingCancellationRequested)
        {
            var flightBooking = await _flightBookingRepository.GetbyOrderIdAsync(flightBookingCancellationRequested.OrderId);

            if (flightBooking is null)
            {
                return;
            }

            flightBooking.Status = FlightBookingStatus.Canceled;

            flightBooking.Events.Add(new FlightBookingEvent
            {
                Name = "Reserva de voo cancelada",
                CreatedAt = DateTime.Now
            });

            await _flightBookingRepository.UpdateAsync(flightBooking);

            await _busService.PublishAsync(new FlightBookingCancelled
            {
                OrderId = flightBooking.OrderId,
                Timestamp = DateTime.UtcNow
            });
        }

        private static FlightBooking TryBooking(FlightBooking flightBooking)
        {
            while (flightBooking.Status != FlightBookingStatus.Confirmed)
            {
                var random = new Random();

                if (random.Next(0, 10) % 2 == 0)
                {
                    flightBooking.Status = FlightBookingStatus.Confirmed;
                    flightBooking.FlightExternalId = GetFlightExternalId();
                    flightBooking.PassengerSeat = $"A{random.Next(1, 10)}";
                    flightBooking.Events.Add(new FlightBookingEvent
                    {
                        Name = "Reserva de voo confirmada",
                        CreatedAt = DateTime.Now
                    });
                }
                else
                {
                    flightBooking.Status = FlightBookingStatus.Canceled;
                    flightBooking.Events.Add(new FlightBookingEvent
                    {
                        Name = "Reserva de voo rejeitada",
                        CreatedAt = DateTime.Now
                    });
                }
            }

            return flightBooking;
        }

        private static string GetDestination(string origin)
        {
            var random = new Random();

            var destinations = new List<string> { "São Paulo", "Rio de Janeiro", "Brasília", "Curitiba", "Belo Horizonte" };
            
            var destination = destinations[random.Next(0, destinations.Count)];

            while (destination == origin)
            {
                destination = destinations[random.Next(0, destinations.Count)];
            }

            return destination;
        }

        public string GetOrigin()
        {
            var random = new Random();
            var origins = new List<string> { "São Paulo", "Rio de Janeiro", "Brasília", "Curitiba", "Belo Horizonte" };
            return origins[random.Next(0, origins.Count)];
        }

        private TimeSpan GenerateHour()
        {
            var random = new Random();
            return new TimeSpan(random.Next(0, 23), random.Next(0, 59), 0);
        }

        private DateTime GenerateDate()
        {
            var random = new Random();
            return DateTime.Now.AddDays(random.Next(1, 10));
        }

        private static string GetFlightExternalId()
        {
            var random = new Random();
            return $"Boeing-{random.Next(1, 10)}";
        }

       
    }
}
