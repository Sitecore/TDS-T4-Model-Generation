using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data;
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
                    var scField = _item.Fields[ID.Parse(key)];
                    if (ItemFactory.FieldWrapperInterfaceMap.ContainsKey(scField.Type.ToLower()))
                    {
                        _fields[key] = this.ItemFactory.TypeContainer.ResolveFieldWrapper(ItemFactory.FieldWrapperInterfaceMap[scField.Type.ToLower()]);
                        _fields[key].Original = scField;
                        _fields[key].ItemFactory = ItemFactory;
                    }
                }
                catch (Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Error instantiating field wrapper", ex, this);
                }
            }
            return _fields[key];
        }

        public BaseItemWrapper()
        {
        }

        public IItemFactory ItemFactory
        {
            get;
            set;
        }

        public string DatabaseName
        {
            get { return _item.Database.Name; }
        }

        public string ItemName
        {
            get { return _item.Name; }
        }
        public string ItemId
        {
            get { return _item.ID.ToString(); }
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
            set { _item = value as Item; }
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
            return ItemFactory.SelectChildrenOfPath<T>(this._item.ID.ToString());
        }
    }
}
