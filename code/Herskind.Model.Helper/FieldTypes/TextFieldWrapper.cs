using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Fields;

namespace Herskind.Model.Helper.FieldTypes
{
    public class TextFieldWrapper : BaseFieldWrapper, ITextFieldWrapper
    {
        public string Render(int lenght, string elipsis)
        {
            throw new NotImplementedException();
        }
    }
}
