﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy_1.Models
{
    internal class Booking
    {
        [JsonProperty("bookingid")]
        public string? BookingId { get; set; }

        [JsonProperty("booking")]
        public BookingDetails BookingDetails { get; set; }
    }
}
