using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSite.Web.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный Email адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public int? Year { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
