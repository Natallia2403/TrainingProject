using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Data.Models
{
    public class EventLog
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string Level { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Exception { get; set; }

        public string Properties { get; set; }

        public string LogEvent { get; set; }
    }
}
