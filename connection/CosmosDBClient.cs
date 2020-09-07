using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace CustomerManagement.connection
{
    public class CosmosDBClient: ICosmosDBClient
    {
        public IConfiguration Configuration { get; }
        public CosmosClient CosmosClient { get; set; }
        public Database CosmosDatabase { get; set; }
        public Container Container { get; set; }

        private readonly string _accountUrl;
        private readonly string _primarykey;
        private readonly string _dataBaseName;
        private readonly string _containerName;
        public CosmosDBClient(IConfiguration configuration)
        {
            _accountUrl = configuration["CosmosDb:Account"];
            _primarykey = configuration["CosmosDb:Key"];
            _dataBaseName = configuration["CosmosDb:DatabaseName"];
            _containerName = configuration["CosmosDb:ContainerName"];
            this.CosmosClient = new CosmosClient(_accountUrl, _primarykey);
            //this.Container = this.CosmosClient.GetContainer(_dataBaseName, _containerName);
            //GetInitialize();
        }
        
        public void GetInitialize()
        {
            this.CosmosClient = new CosmosClient(_accountUrl, _primarykey);
            var dbresponse = this.CosmosClient.CreateDatabaseIfNotExistsAsync(_dataBaseName).GetAwaiter().GetResult();
            this.CosmosDatabase = dbresponse.Database;
        }

    }
}
