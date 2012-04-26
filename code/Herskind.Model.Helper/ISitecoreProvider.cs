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
    }
}
