using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper
{
    public interface IItemFactory
    {
        IContainerProvider TypeContainer { get; set; }
        ISitecoreProvider SitecoreProvider { get; set; }
        IDictionary<string, Type> FieldWrapperInterfaceMap { get; set; }
        IDictionary<string, Type> ItemWrapperInterfaceMap { get; set; }

        T GetContextItem<T>() where T : IItemWrapper;
        T GetSiteHome<T>(IItemWrapper context) where T : IItemWrapper;
        IEnumerable<T> Select<T>(string query, IItemWrapper context) where T : IItemWrapper;
    }
}
