using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Common.Enums;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookingSite.Web.ViewModels
{
    public class HotelViewModel
    {
        public IEnumerable<CountryDTO> Countries { get; set; }

        public Stars Star { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        //[Remote(action: "CheckHotelName", controller: "Manage", ErrorMessage = "Имя отеля уже используется")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указано описание")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Не указан адрес")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Address { get; set; }

        public int Stars { get; set; }

        public bool IsAppartment { get; set; }

        public int CountryId { get; set; }
    }
}
