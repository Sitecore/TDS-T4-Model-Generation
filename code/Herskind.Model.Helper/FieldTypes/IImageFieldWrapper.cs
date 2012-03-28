using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper.FieldTypes
{
    public interface IImageFieldWrapper : IFieldWrapper
    {
        string Render(int width, int height, bool crop);
        string Render(int width, int height, bool crop, string cssClass);
    }
}
