using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper.FieldTypes
{
    public interface IBooleanFieldWrapper : IFieldWrapper
    {
        bool Boolean { get; set; }
    }
}
