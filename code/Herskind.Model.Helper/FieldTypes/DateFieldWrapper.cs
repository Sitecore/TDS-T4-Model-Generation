using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Herskind.Model;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Herskind.Model.Helper.FieldTypes
{
    public class DateFieldWrapper : BaseFieldWrapper, IDateFieldWrapper
    {
        public DateFieldWrapper(Field field)
            : base(field)
        { 
        }

        public string Render(string dateFormat)
        {
            throw new NotImplementedException();
        }

        public DateTime Date
        {
            get
            {
                return new DateField(_field).DateTime;
            }
            set
            {
                RawValue = Sitecore.DateUtil.ToIsoDate(value, true);
            }
        }
    }
}
