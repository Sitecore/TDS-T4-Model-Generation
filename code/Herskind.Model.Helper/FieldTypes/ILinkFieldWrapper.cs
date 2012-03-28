using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper.FieldTypes
{
    public interface ILinkFieldWrapper : IFieldWrapper
    {
        string RenderAroundHtml(string innerHTML);
        T GetTarget<T>() where T : IItemWrapper; 
    }
}
