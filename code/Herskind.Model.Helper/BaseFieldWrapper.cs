using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Web.UI.WebControls;
using Herskind.Model.Helper.FieldTypes;

namespace Herskind.Model.Helper
{
    public class BaseFieldWrapper : IFieldWrapper
    {
        protected bool _modified = false;
        protected Sitecore.Data.Fields.Field _field;


        public BaseFieldWrapper(Sitecore.Data.Fields.Field field)
        {
            Sitecore.Diagnostics.Assert.ArgumentNotNull(field, "field");
            _field = field;
        }

        public string RawValue
        {
            get
            {
                return _field.Value;
            }
            set
            {
                _modified = true;
                _field.Value = value;
            }
        }

        public bool IsModified
        {
            get { return _modified; }
        }

        public string Render()
        {
            return FieldRenderer.Render(_field.Item, _field.Key);
        }

        public string Render(string parameters)
        {
            return FieldRenderer.Render(_field.Item, _field.Key, parameters);
        }

        public object Original
        {
            get { return _field; }
        }
    }
}
