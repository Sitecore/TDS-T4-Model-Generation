using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Fields;
using Sitecore.Web.UI.WebControls;

namespace Herskind.Model.Helper.FieldTypes
{
    public class LinkFieldWrapper : BaseFieldWrapper, ILinkFieldWrapper
    {
        public string RenderAroundHtml(string innerHTML)
        {
            var fieldRenderer = new FieldRenderer();

            fieldRenderer.Item = _field.Item;
            fieldRenderer.FieldName = _field.Key;

            var result = fieldRenderer.RenderField();

            return result.FirstPart + innerHTML + result.LastPart;
        }


        public T GetTarget<T>() where T : IItemWrapper
        {
            if (_field.TypeKey == "droplink")
            {
                var reference = new LookupField(_field);
                return ItemFactory.SelectSinglePath<T>(reference.TargetID.ToString());
            }
            return default(T);
        }
    }
}
