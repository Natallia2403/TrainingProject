using BookingSite.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSite.Web.TagHelpers
{
    public class SortHeaderTagHelper : TagHelper
    {
        /// <summary>
        /// значение текущего свойства, для которого создается тег
        /// </summary>
        public SortState Property { get; set; }

        /// <summary>
        /// значение активного свойства, выбранного для сортировки
        /// </summary>
        public SortState Current { get; set; }

        /// <summary>
        /// действие контроллера, на которое создается ссылка
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// сортировка по возрастанию или убыванию
        /// </summary>
        public bool Up { get; set; }    

        /// <summary>
        /// Для создания адреса ссылки по методу контроллера
        /// </summary>
        private IUrlHelperFactory urlHelperFactory;

        public SortHeaderTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        /// <summary>
        ///  контекст представления ViewContext, в котором будет вызываться хелпер
        ///  С помощью этого объекта мы сможем получить объект IUrlHelper, который необходим для создания ссылки.
        /// </summary>
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";
            string url = urlHelper.Action(Action, new { sortOrder = Property });
            output.Attributes.SetAttribute("href", url);
            // если текущее свойство имеет значение CurrentSort
            if (Current == Property)
            {
                TagBuilder tag = new TagBuilder("i");
                tag.AddCssClass("glyphicon");

                if (Up == true)   // если сортировка по возрастанию
                    tag.AddCssClass("glyphicon-chevron-up");
                else   // если сортировка по убыванию
                    tag.AddCssClass("glyphicon-chevron-down");

                output.PreContent.AppendHtml(tag);
            }
        }
    }
}
