using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace CustomerManagement.connection
{
    public interface ICosmosDBClient
    {
        Database CosmosDatabase { get; }
        CosmosClient CosmosClient { get; }
        void GetInitialize();
    }
}
