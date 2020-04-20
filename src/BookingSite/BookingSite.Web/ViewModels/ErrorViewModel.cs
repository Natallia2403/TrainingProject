using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSite.Web.ViewModels
{
    public class ErrorViewModel
    {
        /// <summary>
        /// Id заявки.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Статус-код + его описание.
        /// </summary>
        public (int code, string description) StatusCode { get; set; }
    }
}
