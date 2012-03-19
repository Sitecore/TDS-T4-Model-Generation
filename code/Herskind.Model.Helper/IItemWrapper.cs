using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper
{
    public interface IItemWrapper : IWrapper
    {
        string DatabaseName { get; }
        string LanguageName { get; }
        string ItemLocation { get; }
        string ItemName { get; }

        void SaveChanges();
        string GenerateUrl();
        string GenerateUrl(bool includeHostname);

        IEnumerable<T> SelectChildren<T>() where T : IItemWrapper;
    }
}
