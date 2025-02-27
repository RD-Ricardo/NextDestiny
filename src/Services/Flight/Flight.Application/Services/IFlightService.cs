using NextDestiny.Core.Shared.Events.Flight;

namespace Flight.Application.Services
{
    public interface IFlightService
    {
        Task BookingAsync(FlightBookingRequested flightBookingRequested);
        Task CancelAsync(FlightBookingCancellationRequested flightBookingCancellationRequested);
    }
}
