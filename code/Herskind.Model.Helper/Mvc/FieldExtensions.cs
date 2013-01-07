using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Herskind.Model.Helper.FieldTypes;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace Herskind.Model.Helper.Mvc
{
    public static class FieldExtensions
    {
        public static HtmlString Render(this IFieldWrapper fieldWrapper)
        {
            return new HtmlString(fieldWrapper.RenderField());
        }
        public static HtmlString Render(this IDateFieldWrapper fieldWrapper, string dateFormat)
        {
            return new HtmlString(fieldWrapper.RenderField("format=" + dateFormat));
        }
        public static HtmlString Render(this IImageFieldWrapper fieldWrapper, int width, int height, bool crop)
        {
            return fieldWrapper.Render(width, height, crop, "");
        }
        public static HtmlString Render(this IImageFieldWrapper fieldWrapper, int width, int height, bool crop, string cssClass)
        {
            return new HtmlString(fieldWrapper.RenderField(string.Format("w={0}&h={1}{2}&class={3}", width, height, crop ? "&crop=1" : "", cssClass)));
        }

        public static HtmlString RenderAroundHtml(this IFieldWrapper fieldWrapper, string innerHtml)
        {
            var fieldRenderer = new FieldRenderer
                                    {
                                        Item = ((Field)fieldWrapper.Original).Item,
                                        FieldName = ((Field)fieldWrapper.Original).Key
                                    };


            var result = fieldRenderer.RenderField();

            return new HtmlString(result.FirstPart + innerHtml + result.LastPart);
        }
    }
}
