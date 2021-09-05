using System;
using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Interfaces{

    public interface IItemRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
        void CreateItem(Item item);
        void UpdateItem(Item item);

        // updated the itemrepository:
        void DeleteItem(Guid id);
    }


}