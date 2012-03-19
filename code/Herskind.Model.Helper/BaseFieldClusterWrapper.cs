using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Herskind.Model.Helper.FieldTypes;

namespace Herskind.Model.Helper
{
    public class BaseFieldClusterWrapper  : IFieldClusterWrapper
    {
        protected Item _item;
        protected Dictionary<string, IFieldWrapper> _fields = new Dictionary<string,IFieldWrapper>();

        protected IFieldWrapper GetField(string key)
        {
            key = key.ToLower();
            if (!_fields.Keys.Contains(key))
            {
                try
                {
                    var scField = _item.Fields[key];
                    switch (scField.Type.ToLower())
                    {
                        case "checkbox":
                            _fields[key] = new BooleanFieldWrapper(scField);
                            break;
                        case "image":
                            _fields[key] = null;
                            // TODO: image
                            //return null;
                            break;
                        case "date":
                        case "datetime":
                            _fields[key] = new DateFieldWrapper(scField);
                            break;
                        case "checklist":
                        case "treelist":
                        case "treelistex":
                        case "multilist":
                            _fields[key] = new ListFieldWrapper(scField);
                            break;
                        case "droplink":
                        case "droptree":
                        case "general link":
                            _fields[key] = new LinkFieldWrapper(scField);
                            break;
                        case "single-line text":
                        case "multi-line text":
                        case "rich text":
                            _fields[key] = new TextFieldWrapper(scField);
                            break;
                        default:
                            _fields[key] = null;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Todo: Log error
                }
            }
            return _fields[key];
        }

        public BaseFieldClusterWrapper(Item item)
        {
            _item = item;
        }

    }
}
