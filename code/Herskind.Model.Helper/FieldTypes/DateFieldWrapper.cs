﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Herskind.Model;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace Herskind.Model.Helper.FieldTypes
{
    public class DateFieldWrapper : BaseFieldWrapper, IDateFieldWrapper
    {
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
