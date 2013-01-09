using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using System.Reflection;

namespace Herskind.Model.Helper
{
    public class ItemFactory : IItemFactory
    {
        public IContainerProvider TypeContainer { get; set; }
        public ISitecoreProvider SitecoreProvider { get; set; }
        public IDictionary<string, Type> FieldWrapperInterfaceMap { get; set; }
        public IDictionary<string, Type> ItemWrapperInterfaceMap { get; set; }

        public ItemFactory()
        {
        }

        public ItemFactory(IContainerProvider typeContainer, IDictionary<string, Type> fieldWrapperInterfaceMap, string itemWrapperAssemblyFilter)
        {
            this.TypeContainer = typeContainer;
            this.FieldWrapperInterfaceMap = fieldWrapperInterfaceMap;
            this.ItemWrapperInterfaceMap = new Dictionary<string, Type>();

            RegisterTemplateMappings(itemWrapperAssemblyFilter);
            RegisterDefaultFieldTypes();
        }

        protected virtual void RegisterDefaultFieldTypes()
        {
            TypeContainer.RegisterFieldWrapper(typeof(FieldTypes.IBooleanFieldWrapper), typeof(FieldTypes.BooleanFieldWrapper));
            TypeContainer.RegisterFieldWrapper(typeof(FieldTypes.IDateFieldWrapper), typeof(FieldTypes.DateFieldWrapper));
            TypeContainer.RegisterFieldWrapper(typeof(FieldTypes.IImageFieldWrapper), typeof(FieldTypes.ImageFieldWrapper));
            TypeContainer.RegisterFieldWrapper(typeof(FieldTypes.ILinkFieldWrapper), typeof(FieldTypes.LinkFieldWrapper));
            TypeContainer.RegisterFieldWrapper(typeof(FieldTypes.IListFieldWrapper), typeof(FieldTypes.ListFieldWrapper));
            TypeContainer.RegisterFieldWrapper(typeof(FieldTypes.ITextFieldWrapper), typeof(FieldTypes.TextFieldWrapper));
        }

        protected virtual void RegisterTemplateMappings(string assemblyFilter)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.StartsWith(assemblyFilter))
                {
                    foreach (Type t in assembly.GetTypes())
                    {
                        foreach (TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
                        {
                            if (!ItemWrapperInterfaceMap.ContainsKey(templateAttribute.Id))
                            {
                                ItemWrapperInterfaceMap.Add(templateAttribute.Id, templateAttribute.Interface);
                                TypeContainer.RegisterItemWrapper(templateAttribute.Interface, t);
                            }
                        }
                    }
                }
            }
        }

        public T GetSiteHome<T>(IItemWrapper context) where T : IItemWrapper
        {
            var item = SitecoreProvider.GetSiteHome(context.Original as Item);
            var wrapper = SpawnNullableTypeFromItem(item, typeof(T));
            return (T)((wrapper is T) ? wrapper : null);

        }

        public T GetContextItem<T>() where T : IItemWrapper
        {
            var item = SitecoreProvider.GetContextItem();
            var wrapper = SpawnNullableTypeFromItem(item, typeof(T));
            return (T)((wrapper is T) ? wrapper : null);
        }


        public IEnumerable<T> Select<T>(string query, IItemWrapper context) where T : IItemWrapper
        {
            var items = (context == null) ? SitecoreProvider.SelectItems(query, null) : SitecoreProvider.SelectItems(query, context.Original as Item);
            return FilterWrapperTypes<T>(SpawnTypeFromItemList(items));
        }

        public T SelectSingle<T>(string query, IItemWrapper context) where T : IItemWrapper
        {
            var items = (context == null) ? SitecoreProvider.SelectItems(query, null) : SitecoreProvider.SelectItems(query, context.Original as Item);
            if (items == null || items.Count() == 0)
                return (T)SpawnNullableTypeFromItem(null, typeof(T));
            else
                return (T)SpawnNullableTypeFromItem(items.First(), typeof(T));
        }

        private IEnumerable<IItemWrapper> SpawnTypeFromItemList(IEnumerable<Item> items)
        {
            if (items != null)
            {
                return items.Select(i => SpawnTypeFromItem(i)).Where(w => w != null);
            }
            return new List<IItemWrapper>();
        }

        private IEnumerable<T> FilterWrapperTypes<T>(IEnumerable<IItemWrapper> wrappers)
        {
            foreach(var wrapper in wrappers.Where(w => w is T).ToList())
            {
                yield return (T)wrapper;
            }
        }

        private IItemWrapper SpawnNullableTypeFromItem(Item item, Type t)
        {
            if (item != null)
            {
                return SpawnTypeFromItem(item);
            }
            else
            {
                return SpawnTypeNullItem(t);
            }
        }

        private IItemWrapper SpawnTypeFromItem(Item item)
        {
            if (item != null)
            {
                if (ItemWrapperInterfaceMap.ContainsKey(item.TemplateID.ToString()))
                {
                    var itemWrapper = TypeContainer.ResolveItemWrapper(ItemWrapperInterfaceMap[item.TemplateID.ToString()]);
                    itemWrapper.Original = item;
                    itemWrapper.ItemFactory = this;
                    return itemWrapper;
                }
            }
            return null;
        }

        private IItemWrapper SpawnTypeNullItem(Type t)
        {
            var itemWrapper = TypeContainer.ResolveItemWrapper(t);
            itemWrapper.ItemFactory = this;
            return itemWrapper;
        }
    }
}
    