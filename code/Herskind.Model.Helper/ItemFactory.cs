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
        private static ItemFactory _instance;
        public static IItemFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemFactory();
                }
                return _instance;
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

        public IEnumerable<T> SelectPath<T>(string path) where T : IItemWrapper
        {
            // TODO: Ensure item exists
            var items = Sitecore.Context.Database.SelectItems(path);
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
            var item = Sitecore.Context.Item;
            var wrapper = SpawnTypeFromItem(item);
            return (T)((wrapper is T) ? wrapper : null);
        }

        private IEnumerable<IItemWrapper> SpawnTypeFromItemList(IEnumerable<Item> items)
        {
            return items.Select(i => SpawnTypeFromItem(i));
        }

        private IEnumerable<T> FilterWrapperTypes<T>(IEnumerable<IItemWrapper> wrappers)
        {
            return (IEnumerable<T>)(wrappers.Where(w => w is T));
        }

        private IItemWrapper SpawnTypeFromItem(Item item)
        {
            var id = item.TemplateID.ToString();
            if (TemplateMap.Keys.Contains(id))
            {
                // Get type information
                var type = TemplateMap[id];
                // Get public constructors
                var ctors = type.GetConstructors();
                // Invoke the first public constructor with no parameters.
                return (IItemWrapper)ctors[0].Invoke(new object[] { item });
            }
            return new BaseItemWrapper(item);
        }

        private Dictionary<string, Type> _templateMap = null;
        private Dictionary<string, Type> TemplateMap 
        {
            get
            {
                if (_templateMap == null)
                {
                    _templateMap = new Dictionary<string, Type>();
                    var assembly = System.Reflection.Assembly.GetCallingAssembly();
                    foreach (Type t in assembly.GetTypes())
                    {
                        foreach (TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
                        {
                            if (!_templateMap.Keys.Contains(templateAttribute.Id))
                            {
                                _templateMap.Add(templateAttribute.Id, t);
                            }
                        }
                    }
                }
                return _templateMap;
            }
        }
    }
}
    