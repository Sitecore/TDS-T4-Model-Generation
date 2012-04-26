using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Herskind.Model.Helper.Unity
{
    public class ContainerProvider : IContainerProvider
    {
        private UnityContainer container = new UnityContainer();

        public void RegisterItemWrapper(Type TInterface, Type TImplementation)
        {
            container.RegisterType(TInterface, TImplementation, new PerResolveLifetimeManager());
        }

        public IItemWrapper ResolveItemWrapper(Type TInterface) 
        {
            return container.Resolve(TInterface) as IItemWrapper;
        }

        public void RegisterFieldWrapper(Type TInterface, Type TImplementation)
        {
            container.RegisterType(TInterface, TImplementation, new PerResolveLifetimeManager());
        }

        public IFieldWrapper ResolveFieldWrapper(Type TInterface)
        {
            return container.Resolve(TInterface) as IFieldWrapper;
        }
    }
}
