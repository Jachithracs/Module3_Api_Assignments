using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy_1.Models
{
    internal class GetBookingId
    {
        [JsonProperty("bookingid")]
        public string? BookingId { get; set; }
    }
}
