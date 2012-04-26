using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;

namespace Herskind.Model.Helper
{
    public class SitecoreProvider : ISitecoreProvider
    {
        public SitecoreProvider()
        {
        }

        public Item GetContextItem()
        {
            return Sitecore.Context.Item;
        }
    }
}
