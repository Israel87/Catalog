using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<ItemDTO> Getitems()
        {
            return _inMemoryItem.GetItems().Select(item => item.AsDTO());
        }

        // GET / items / id
        [HttpGet("{Id}")]
        public ActionResult<ItemDTO> GetItem(Guid Id)
        {
            var _items = _inMemoryItem.GetItem(Id);
            if (_items is null)
            {
                return NotFound();
            }
            return Ok(_items.AsDTO());
        }


        // POST / items 
        [HttpPost]
        public ActionResult<ItemDTO> CreateItem(CreateItemDTO createItem)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = createItem.Name,
                Price = createItem.Price,
                CreatedDate = DateTimeOffset.Now
            };

            _inMemoryItem.CreateItem(item);

            return CreatedAtAction(nameof(Getitems), new Item { Id = item.Id }, item.AsDTO());

        }

        // PUT / items / {id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateDTO updateDTO)
        {
            var existingItem = _inMemoryItem.GetItem(id);

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

            _inMemoryItem.UpdateItem(update);

            return NoContent();
        }


        // DELETE / items / {id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id){
            var existingItem = _inMemoryItem.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            _inMemoryItem.DeleteItem(id);

            return NoContent();

        }

    }
}