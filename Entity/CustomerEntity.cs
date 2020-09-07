using Newtonsoft.Json;

namespace CustomerManagement.Entity
{
    public class CustomerEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "address")]
        public AddressEntity Address { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public long Phone { get; set; }
    }
}
