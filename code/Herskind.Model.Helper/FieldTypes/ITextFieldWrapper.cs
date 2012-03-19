using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper.FieldTypes
{
    public interface ITextFieldWrapper : IFieldWrapper
    {
        string Render(int lenght, string elipsis);
    }
}
