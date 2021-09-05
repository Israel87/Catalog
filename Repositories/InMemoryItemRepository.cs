using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;
using Catalog.Interfaces;

namespace Catalog.Repositories
{
    public class InMemoryItemRepository : IItemRepository
    {
        // In Memory collection in place of Database DbContext for now:
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Portion", Price = 9, CreatedDate = DateTimeOffset.Now },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 24, CreatedDate = DateTimeOffset.Now },
            new Item { Id = Guid.NewGuid(), Name = "Brown Shield", Price = 7, CreatedDate = DateTimeOffset.Now }
        };


        // get list of Items.
        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        // get items by Id.
        public Item GetItem(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreateItem(Item item)
        {
             items.Add(item);
        }

        public void UpdateItem(Item item)
        {
           var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
           items[index] = item;
        }

        public void DeleteItem(Guid id)
        {
             var index = items.FindIndex(existingItem => existingItem.Id == id);
             items.RemoveAt(index);
        }
    }
}