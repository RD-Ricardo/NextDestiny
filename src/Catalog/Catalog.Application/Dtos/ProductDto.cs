using Catalog.Domain.Enums;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public IList<string> Images { get; set; } = [];
        public IList<Feedback> Feedbacks { get; set; } = [];
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public LocationDto Location { get; set; } = null!;
        public Guid HotelId { get; set; }
    }

    public class LocationDto
    {
        public Guid Id { get; set; }
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
