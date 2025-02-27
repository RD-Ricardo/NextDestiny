using Hotel.Domain.Domain;
using System;
using Hotel.Domain.Repositoties;
using NextDestiny.Core.Amqp.Abstractions;
using NextDestiny.Core.Shared.Events.Hotel;

namespace Hotel.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBusService _busService;

        private readonly IBookingRepository _bookingRepository;

        private static Random _random = new Random();

        public BookingService(IBusService busService, IBookingRepository bookingRepository)
        {
            _busService = busService;
            _bookingRepository = bookingRepository;
        }

        public async Task BookingAsync(HotelBookingRequested hotelBookingRequested)
        {
            var roomNumber = _random.Next(1, 101);

            var pricePerNight = _random.Next(50, 300);

            var checkInAt = DateTime.Now.AddDays(2);

            var checkOutAt = DateTime.Now.AddDays(4);

            var totalPrice = pricePerNight * (checkInAt - checkOutAt).Days;

            var status = _random.Next(0, 1);

            var orderId = hotelBookingRequested.OrderId;

            var booking = new Booking(
                hotelBookingRequested.CustomerEmail,
                checkInAt,
                checkOutAt,
                roomNumber,
                totalPrice,
                (BookingStatus)status,
                orderId
            );

            await _bookingRepository.CreateAsync(booking);

            if (booking.Status == BookingStatus.Confirmed)
            {
                await _busService.PublishAsync(new HotelBookingCompleted(orderId));
            }

            if (booking.Status == BookingStatus.Failed)
            {
                await _busService.PublishAsync(new HotelBookingFailed(orderId));
            }
        }

        public async Task CancelAsync(HotelBookingCancellationRequested hotelBookingCancellationRequested)
        {
            var booking = await _bookingRepository.GetByOrderIdAsync(hotelBookingCancellationRequested.OrderId);
            
            if (booking == null)
            {
                throw new Exception("Booking not found");
            }
            
            booking.Status = BookingStatus.Cancelled;
            
            await _bookingRepository.UpdateAsync(booking);

            await _busService.PublishAsync(new HotelBookingCancelled(hotelBookingCancellationRequested.OrderId));
        }
    }
}
