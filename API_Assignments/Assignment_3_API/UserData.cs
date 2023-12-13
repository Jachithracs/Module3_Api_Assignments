using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3_API
{
    public class UserData
    {
        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("Id")]
        public string? Id { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("body")]
        public string? Body { get; set; }

    }

}
