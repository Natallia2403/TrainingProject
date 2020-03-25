using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        public DateTime Date { get; set; }

        public int Amount { get; set; }

        public int BookingId { get; set; }

        public Booking Booking { get; set; }
    }
}
