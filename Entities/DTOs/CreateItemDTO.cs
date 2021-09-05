using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOs
{
    public record CreateItemDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1, 1000)]
        public Decimal Price { get; init; }

    }
}