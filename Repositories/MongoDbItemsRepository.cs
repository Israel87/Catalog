using System;
using System.Collections.Generic;
using Catalog.Entities;
using Catalog.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemRepository
    {
        // Set this in the appsettings.json and retrieve using Configuration Manager
        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemscollection;
        

        // Fitler feature for MongoDB.
        private readonly FilterDefinitionBuilder<Item> filterDefinitionBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemscollection = database.GetCollection<Item>(collectionName);

        }
        public void CreateItem(Item item)
        {
            itemscollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
            itemscollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
            return itemscollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemscollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterDefinitionBuilder.Eq(existingItem => existingItem.Id, item.Id);
            itemscollection.ReplaceOne(filter, item);
        }
    }
}