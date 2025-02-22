using NextDestiny.Core.Shared.Events.Flight;

namespace Flight.Application.Services
{
    public interface IFlightService
    {
        Task Booking(FlightBookingRequested flightBookingRequested);
    }
}
