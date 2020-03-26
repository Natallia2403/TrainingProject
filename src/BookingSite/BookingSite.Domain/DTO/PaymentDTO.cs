using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Domain.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        public DateTime Date { get; set; }

        public int Amount { get; set; }

        public int BookingId { get; set; }

        public BookingDTO Booking { get; set; }
    }
}
