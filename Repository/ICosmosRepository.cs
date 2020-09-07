using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    public interface ICosmosRepository
    {
        Task<IEnumerable<T>> GetItemsAsync<T>(string queryString);
        Task<bool>  AddItemAsync<T>(T item);
        Task<T> UpdateItemAsync<T>(string id, T item) where T : class;
        Task<bool> DeleteItemAsync<T>(string id) where T : class;
    }
}

