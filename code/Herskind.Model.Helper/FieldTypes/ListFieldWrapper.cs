using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Herskind.Model.Helper.FieldTypes
{
    public class ListFieldWrapper : BaseFieldWrapper, IListFieldWrapper
    {
        public IEnumerable<IItemWrapper> Items
        {
            get
            {
                IItemFactory factory = new ItemFactory();
                var listField = (MultilistField)_field;
                foreach (var id in listField.Items)
                {
                    yield return factory.Select<IItemWrapper>(id, null).First();
                }
            }
        }
    }
}
