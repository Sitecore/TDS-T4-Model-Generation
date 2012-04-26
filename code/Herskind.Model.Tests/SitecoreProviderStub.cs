using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Herskind.Model.Helper;

namespace Herskind.Model.Tests
{
    public class SitecoreProviderStub : ISitecoreProvider
    {
        public Sitecore.Data.Items.Item GetContextItem()
        {
            return new SampleItem(new Sitecore.Data.FieldList());
        }
    }
}
