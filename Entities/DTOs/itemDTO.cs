using System;

namespace Catalog.Entities.DTOs{
     public record ItemDTO {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}