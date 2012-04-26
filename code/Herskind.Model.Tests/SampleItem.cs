using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Globalization;

namespace Herskind.Model.Tests
{
    public class SampleItem : Item
    {
        public SampleItem(FieldList fieldList, string itemName = "Sample Item", string templateId = "{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}") :
            base(ID.NewID, new ItemData(new ItemDefinition(new ID(new Guid()), itemName, new ID(Guid.Parse(templateId)), new ID(new Guid())), Language.Invariant, new Sitecore.Data.Version(1), fieldList), new Database("web"))
        {
        }
    }
}
