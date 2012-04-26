using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Fields;

namespace Herskind.Model.Helper.FieldTypes
{
    public class ImageFieldWrapper : BaseFieldWrapper, IImageFieldWrapper
    {
        public string Render(int width, int height, bool crop, string cssClass)
        {
            return base.Render(string.Format("w={0}&h={1}{2}&class={3}", width, height, crop ? "&crop=1" : "", cssClass));
        }

        public string Render(int width, int height, bool crop)
        {
            return this.Render(width, height, crop, null);
        }
    }
}
