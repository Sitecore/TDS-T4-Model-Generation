using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper
{
    public interface IItemFactory
    {

        T GetSiteHome<T>() where T : IItemWrapper;
        T GetContextItem<T>() where T : IItemWrapper;
        T SelectSinglePath<T>(string path) where T : IItemWrapper;
        T SelectSinglePath<T>(string path, IItemWrapper context) where T : IItemWrapper;
        IEnumerable<T> SelectPath<T>(string path) where T : IItemWrapper;
        IEnumerable<T> SelectPath<T>(string path, IItemWrapper context) where T : IItemWrapper;
        IEnumerable<T> SelectChildrenOfPath<T>(string path) where T : IItemWrapper;
    }
}
