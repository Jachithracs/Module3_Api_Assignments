using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy_1.Models
{
    public class BookingDetails
    {
        [JsonProperty("bookingid")]
        public string? BookingId { get; set; }

        [JsonProperty("firstname")]
        public string? FirstName { get; set; }

        [JsonProperty("lastname")]
        public string? LastName { get; set; }

        [JsonProperty("totalprice")]
        public int? TotalPrice { get; set; }

        [JsonProperty("depositpaid")]
        public int? DepositPaid { get; set; }

        [JsonProperty("bookingdates")]
        public BookingDates BookingDates { get; set; }

        [JsonProperty("additionalneeds")]
        public string? AdditionalNeeds { get; set; }


    }
}
