using Newtonsoft.Json;

namespace CustomerManagement.Models
{
    public class Customer
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public long Phone { get; set; }
    }
}
