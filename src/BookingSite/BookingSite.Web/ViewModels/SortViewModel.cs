using BookingSite.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSite.Web.ViewModels
{
    public class SortViewModel
    {
        /// <summary>
        /// значение для сортировки по имени
        /// </summary>
        public SortState NameSort { get; set; } 

        /// <summary>
        /// значение для сортировки по звездам
        /// </summary>
        public SortState StarsSort { get; set; }    

        /// <summary>
        /// значение для сортировки по странам
        /// </summary>
        public SortState CountrySort { get; set; }   

        /// <summary>
        /// значение свойства, выбранного для сортировки
        /// Оно нужно лишь для того, чтобы в tag-хелпере определить, 
        /// что данное свойство, для которого применяется хелпер, используется в текущий момент для сортировки.
        /// Поэтому свойство Current указывает на значение текущего выбраного свойства, 
        /// по которому проводится сортировка. 
        /// То есть свойство Current будет равно одну из свойств NameSort, AgeSort или CountrySort
        /// </summary>
        public SortState Current { get; set; }

        /// <summary>
        /// Сортировка по возрастанию или убыванию
        /// </summary>
        public bool Up { get; set; }  

        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            NameSort = SortState.NameAsc;
            StarsSort = SortState.StarsAsc;
            CountrySort = SortState.CountryAsc;
            Up = true;

            if (sortOrder == SortState.StarsDesc || sortOrder == SortState.NameDesc
                || sortOrder == SortState.CountryDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    Current = NameSort = SortState.NameAsc;
                    break;
                case SortState.StarsAsc:
                    Current = StarsSort = SortState.StarsDesc;
                    break;
                case SortState.StarsDesc:
                    Current = StarsSort = SortState.StarsAsc;
                    break;
                case SortState.CountryAsc:
                    Current = CountrySort = SortState.CountryDesc;
                    break;
                case SortState.CountryDesc:
                    Current = CountrySort = SortState.CountryAsc;
                    break;
                default:
                    Current = NameSort = SortState.NameDesc;
                    break;
            }
        }
    }
}
