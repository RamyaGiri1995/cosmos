using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using CustomerManagement.connection;
using Microsoft.Extensions.Configuration;

namespace CustomerManagement.Repository
{
    public class CosmosRepository : ICosmosRepository
    {
        private Container _container;

        public CosmosRepository(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }
       

        public async Task<bool> AddItemAsync<T>(T item)
        {
            if (item != null)
            {
                await this._container.CreateItemAsync<T>(item);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteItemAsync<T>(string id) where T : class
        {
            if (id != null)
            {
                await this._container.DeleteItemAsync<T>(id, new PartitionKey(id));
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(string queryString)
        {
            var query = this._container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<T> UpdateItemAsync<T>(string id, T item) where T : class
        {
            var result = await this._container.ReplaceItemAsync<T>(item, id, new PartitionKey(id));
            return result;
        }
    }
}