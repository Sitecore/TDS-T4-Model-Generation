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

        public IEnumerable<T> SelectChildrenOfPath<T>(string path) where T : IItemWrapper
        {
            // TODO: Ensure item exists
            var item = Sitecore.Context.Database.SelectSingleItem(path);
            return FilterWrapperTypes<T>(SpawnTypeFromItemList(item.Children.AsEnumerable()));
        }

        public T SelectSinglePath<T>(string path) where T : IItemWrapper
        {
            // TODO: Ensure item exists
            var item = Sitecore.Context.Database.SelectSingleItem(path);
            var wrapper = SpawnTypeFromItem(item);
            return (T)((wrapper is T) ? wrapper : null);
        }

        public T SelectSinglePath<T>(string path, IItemWrapper context) where T : IItemWrapper
        {
            // TODO: Ensure item exists
            var item = ((Item)context.Original).Axes.SelectSingleItem(path);
            var wrapper = SpawnTypeFromItem(item);
            return (T)((wrapper is T) ? wrapper : null);
        }

        public IEnumerable<T> SelectPath<T>(string path) where T : IItemWrapper
        {
            // TODO: Ensure item exists
            var items = Sitecore.Context.Database.SelectItems(path);
            return FilterWrapperTypes<T>(SpawnTypeFromItemList(items));
        }

        public IEnumerable<T> SelectPath<T>(string path, IItemWrapper context) where T : IItemWrapper
        {
            var items = ((Item)context.Original).Axes.SelectItems(path);
            return FilterWrapperTypes<T>(SpawnTypeFromItemList(items));
        }

        public T GetSiteHome<T>() where T : IItemWrapper
        {
            var item = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var wrapper = SpawnTypeFromItem(item);
            return (T)((wrapper is T) ? wrapper : null);
        }

        public T GetContextItem<T>() where T : IItemWrapper
        {
            var item = SitecoreProvider.GetContextItem();
            var wrapper = SpawnTypeFromItem(item);
            return (T)((wrapper is T) ? wrapper : null);
        }

        private IEnumerable<IItemWrapper> SpawnTypeFromItemList(IEnumerable<Item> items)
        {
            if (items != null)
            {
                return items.Select(i => SpawnTypeFromItem(i));
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

        //private Dictionary<string, Type> _templateMap = null;
        //private Dictionary<string, Type> TemplateMap 
        //{
        //    get
        //    {
        //        if (_templateMap == null)
        //        {
        //            _templateMap = new Dictionary<string, Type>();

        //            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        //            {
        //                try
        //                {
        //                    foreach (Type t in assembly.GetTypes())
        //                    {
        //                        foreach (TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
        //                        {
        //                            if (!_templateMap.Keys.Contains(templateAttribute.Id))
        //                            {
        //                                _templateMap.Add(templateAttribute.Id, t);
        //                            }
        //                        }
        //                    }
        //                }
        //                catch
        //                {
        //                    // Log error
        //                }
        //            }
        //        }
        //        return _templateMap;
        //    }
        //}
    }
}
    