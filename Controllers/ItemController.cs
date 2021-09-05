using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Entities.DTOs;
using Catalog.Interfaces;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _inMemoryItem;
        public ItemController(IItemRepository itemRepository)
        {
            _inMemoryItem = itemRepository;
        }

        // GET / items 
        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> GetitemsAsync()
        {
            return (await _inMemoryItem.GetItemsAsync()).Select(item => item.AsDTO());
        }

        // GET / items / id
        [HttpGet("{Id}")]
        public async Task<ActionResult<ItemDTO>> GetItemAsync(Guid Id)
        {
            var _items = await _inMemoryItem.GetItemAsync(Id);
            if (_items is null)
            {
                return NotFound();
            }
            return Ok(_items.AsDTO());
        }


        // POST / items 
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItemAsync(CreateItemDTO createItem)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = createItem.Name,
                Price = createItem.Price,
                CreatedDate = DateTimeOffset.Now
            };

            await _inMemoryItem.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetitemsAsync), new Item { Id = item.Id }, item.AsDTO());

        }

        // PUT / items / {id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateDTO updateDTO)
        {
            var existingItem = await _inMemoryItem.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            // New Syntax:
            Item update = existingItem with
            {
                Name = updateDTO.Name,
                Price = updateDTO.Price
            };

            await _inMemoryItem.UpdateItemAsync(update);

            return NoContent();
        }


        // DELETE / items / {id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id){
            var existingItem = await _inMemoryItem.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            await _inMemoryItem.DeleteItemAsync(id);

            return NoContent();

        }

    }
}