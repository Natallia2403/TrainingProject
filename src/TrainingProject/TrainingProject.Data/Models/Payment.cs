using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingProject.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public int BookingId { get; set; }

        public Booking Booking { get; set; }
    }
}
