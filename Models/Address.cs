using System;
using Newtonsoft.Json;

namespace CustomerManagement.Models
{
    public class Address
    {
        [JsonProperty(PropertyName = "houseNumber")]
        public string HouseNumber { get; set; }
        [JsonProperty(PropertyName = "addressLine1")]
        public string AddressLine1 { get; set; }
        [JsonProperty(PropertyName = "addressLine2")]
        public string AddressLine2 { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "zipcode")]
        public long Zipcode { get; set; }
    }
}
