using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Herskind.Model.Sample;

namespace Herskind.Model
{
    public static class SampleExtensionMethods
    {
        public static string FallbackTitle(this ISampleItem itemWrapper)
        {
            if (!string.IsNullOrEmpty(itemWrapper.Title.RawValue))
            {
                return itemWrapper.Title.RawValue;
            }
            return itemWrapper.ItemName;
        }
    }
}
