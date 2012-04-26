using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper
{
    public interface IContainerProvider
    {
        void RegisterItemWrapper(Type TInterface, Type TImplementation);
        IItemWrapper ResolveItemWrapper(Type TInterface);

        void RegisterFieldWrapper(Type TInterface, Type TImplementation);
        IFieldWrapper ResolveFieldWrapper(Type TInterface);
    }
}
