using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Sitecore.Web.UI.WebControls;
using Herskind.Model.Helper.FieldTypes;

namespace Herskind.Model.Helper
{
    public class BaseFieldWrapper : IHtmlString, IFieldWrapper
    {
        protected bool _modified = false;
        protected Sitecore.Data.Fields.Field _field;
        protected IItemFactory _itemFactory;

        //public BaseFieldWrapper(Sitecore.Data.Fields.Field field, IItemFactory itemFactory)
        //{
        //    Sitecore.Diagnostics.Assert.ArgumentNotNull(field, "field");
        //    Sitecore.Diagnostics.Assert.ArgumentNotNull(itemFactory, "itemFactory");
        //    _field = field;
        //    _itemFactory = itemFactory;
        //}

        public string RawValue
        {
            get
            {
                if (_field != null)
                {
                    return _field.Value;
                }
                return "";
            }
            set
            {
                _modified = true;
                _field.Value = value;
            }
        }

        public override string ToString()
        {
            return RenderField();
        }
       
        public string  ToHtmlString()
        {
            return RenderField();
        }

        public bool IsModified
        {
            get { return _modified; }
        }

        public string RenderField()
        {
            if (_field != null)
            {
                return FieldRenderer.Render(_field.Item, _field.Key);
            }
            return "";
        }

        public string RenderField(string parameters)
        {
            if (_field != null)
            {
                return FieldRenderer.Render(_field.Item, _field.Key, parameters);
            }
            return "";
        }

        public IItemFactory ItemFactory
        {
            get;
            set;
        }

        public object Original
        {
            get { return _field; }
            set { _field = value as Sitecore.Data.Fields.Field; }
        }
    }
}
