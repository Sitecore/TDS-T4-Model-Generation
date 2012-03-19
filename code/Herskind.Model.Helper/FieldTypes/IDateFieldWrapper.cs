using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper.FieldTypes
{
    public interface IDateFieldWrapper : IFieldWrapper
    {
        DateTime Date { get; set; }
        string Render(string dateFormat);
    }
}
