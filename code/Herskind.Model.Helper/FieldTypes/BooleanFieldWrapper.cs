using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Fields;

namespace Herskind.Model.Helper.FieldTypes
{
    public class BooleanFieldWrapper : BaseFieldWrapper, IBooleanFieldWrapper
    {
        public BooleanFieldWrapper(Field field)
            : base(field)
        {
        }

        public bool Boolean
        {
            get
            {
                return RawValue == "1";
            }
            set
            {
                RawValue = value ? "1" : "";
            }
        }
    }
}
