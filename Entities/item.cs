using System;

namespace Catalog.Entities {

    // Using the new record type and init attribute.
    public record Item {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}