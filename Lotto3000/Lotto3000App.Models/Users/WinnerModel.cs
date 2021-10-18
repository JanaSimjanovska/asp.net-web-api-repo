using Lotto3000App.Models.Tickets;
using Lotto3000App.Shared.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Lotto3000App.Models.Users
{
    public class WinnerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public List<int> WinningCombination { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Prize Prize { get; set; }
    }
}
