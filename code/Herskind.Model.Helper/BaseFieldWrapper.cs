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
