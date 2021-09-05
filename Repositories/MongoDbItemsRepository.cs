using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task CreateItemAsync(Item item)
        {
            await itemscollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
            await itemscollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
            return await itemscollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemscollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterDefinitionBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await itemscollection.ReplaceOneAsync(filter, item);
        }
    }
}