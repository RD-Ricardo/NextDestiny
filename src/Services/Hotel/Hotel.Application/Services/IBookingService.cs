using NextDestiny.Core.Shared.Events.Hotel;

namespace Hotel.Application.Services
{
    public interface IBookingService
    {
        Task BookingAsync(HotelBookingRequested hotelBookingRequested);
        Task CancelAsync(HotelBookingCancellationRequested hotelBookingCancellationRequested);
    }
}
