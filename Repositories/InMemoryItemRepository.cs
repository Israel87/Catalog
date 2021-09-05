using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        // get items by Id.
        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
           var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
           items[index] = item;
           await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
             var index = items.FindIndex(existingItem => existingItem.Id == id);
             items.RemoveAt(index);
             await Task.CompletedTask;
        }
    }
}