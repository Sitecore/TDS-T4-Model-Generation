using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;

namespace Herskind.Model.Helper
{
    public interface ISitecoreProvider
    {
        Item GetContextItem();
        Item GetSiteHome(Item context);
        IEnumerable<Item> SelectItems(string query, Item context);
    }
}
