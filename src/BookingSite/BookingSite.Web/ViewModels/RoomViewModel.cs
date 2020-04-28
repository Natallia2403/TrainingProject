using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Web.ViewModels
{
    public class RoomViewModel
    {
        public IEnumerable<HotelDTO> Hotels { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано описание")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Не указано максимальное количество гостей")]
        [Range(1, 20, ErrorMessage = "Не более 20 человек")]
        public int? MaxNumberOfGuests { get; set; }

        [Required(ErrorMessage = "Не указана цена")]
        public int? Price { get; set; }

        public bool HasBalcony { get; set; }

        public bool HasKitchen { get; set; }

        public int HotelId { get; set; }
    }
}
