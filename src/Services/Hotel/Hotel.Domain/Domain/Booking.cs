using MongoDB.Bson.Serialization.Attributes;
using NextDestiny.Core.DomainObjects;

namespace Hotel.Domain.Domain
{
    public class Booking : BaseEntity
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid OrderId { get; set; }
        public string CustomerEmail { get; set; } 
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }

        public Booking(string customerEmail, DateTime checkInDate, DateTime checkOutDate, int roomNumber, decimal totalPrice, BookingStatus status, Guid orderId)
        {
            Id = Guid.NewGuid();
            CustomerEmail = customerEmail;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            RoomNumber = roomNumber;
            TotalPrice = totalPrice;
            Status = status;
            OrderId = orderId;
        }

        public int GetStayDuration()
        {
            return (CheckOutDate - CheckInDate).Days;
        }

        public override string ToString()
        {
            return $"Reserva {Id}: {CustomerEmail}, Quarto {RoomNumber}, {GetStayDuration()} dias, Preço Total: {TotalPrice:C}, Status: {Status}, Ordem ID: {OrderId}";
        }
    }

    public enum BookingStatus
    {
        Failed = 0,
        Confirmed = 1,
        Cancelled = 2
    }
}
