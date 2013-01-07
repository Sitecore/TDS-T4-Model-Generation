using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Data;

namespace Herskind.Model.Helper
{
    public class SitecoreProvider : ISitecoreProvider
    {
        protected Database _database = null;

        private Database SitecoreDatabase
        {
            get
            {
                //if (_database != null)
                //{
                //    return _database;
                //}
                return Sitecore.Context.Database;
            }
        }

        public SitecoreProvider()
        {
        }
        
        public SitecoreProvider(string databaseName)
        {
            _database = Sitecore.Configuration.Factory.GetDatabase(databaseName);
        }

        public Item GetContextItem()
        {
            if (SitecoreDatabase.Name == Sitecore.Context.Item.Database.Name)
            {
                return Sitecore.Context.Item;
            }
            return null;
        }

        public Item GetSiteHome(Item context)
        {
            if (SitecoreDatabase.Name == context.Database.Name)
            {
                return SitecoreDatabase.GetItem(Sitecore.Context.Site.StartPath);
            }
            return null;
        }

        public IEnumerable<Item> SelectItems(string query, Item context)
        {
            if (context != null && SitecoreDatabase.Name == context.Database.Name)
            {
                return context.Axes.SelectItems(query);
            }
            return SitecoreDatabase.SelectItems(query);
        }
    }
}
