using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper
{
    public interface IFieldWrapper : IWrapper
    {
        string RawValue { get; set; }
        bool IsModified { get; }
        string RenderField();
        string RenderField(string parameters);
    }
}
