using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Herskind.Model.Helper.FieldTypes;

namespace Herskind.Model.Helper
{
    public class BaseItemWrapper : IItemWrapper
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

        public BaseItemWrapper(Item item)
        {
            _item = item;
        }

        public string DatabaseName
        {
            get { return _item.Database.Name; }
        }

        public string ItemName
        {
            get { return _item.Name; }
        }

        public string LanguageName
        {
            get { return _item.Language.Name; }
        }

        public string ItemLocation
        {
            get { return _item.Paths.FullPath; }
        }

        public void SaveChanges()
        {
            if (_item.Editing.IsEditing)
            {
                _item.Editing.EndEdit();
            }
        }

        public object Original
        {
            get { return _item; }
        }

        public string GenerateUrl()
        {
            return Sitecore.Links.LinkManager.GetItemUrl(_item);
        }

        public string GenerateUrl(bool includeHostname)
        {
            var options = Sitecore.Links.LinkManager.GetDefaultUrlOptions();
            options.AlwaysIncludeServerUrl = true;
            return Sitecore.Links.LinkManager.GetItemUrl(_item, options);
        }

        public IEnumerable<T> SelectChildren<T>() where T : IItemWrapper 
        {
            IItemFactory factory = ItemFactory.Instance;
            return factory.SelectChildrenOfPath<T>(this._item.ID.ToString());
        }
    }
}
