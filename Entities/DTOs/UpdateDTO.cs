using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOs
{
    public record UpdateDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1, 1000)]
        public Decimal Price { get; init; }

    }
}