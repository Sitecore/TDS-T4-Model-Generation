using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Herskind.Model.Helper;
using Herskind.Model.Helper.Unity;
using Herskind.Model.Sample;
using Moq;
using Sitecore.Data;

namespace Herskind.Model.Tests
{
    [TestClass]
    public class UnityTests
    {
        [TestMethod]
        public void ResolveSampleItem()
        {
            // Arrange
            var mock = new Mock<ISitecoreProvider>();
            mock.Setup(sp => sp.GetContextItem())
                .Returns(new SampleItem(new FieldList(){ { ID.Parse("{75577384-3C97-45DA-A847-81B00500E250}"),"Title" }, { ID.Parse("{A60ACD61-A6DB-4182-8329-C957982CEC74}"),"<p>text</p>" } }));
            var container = new UnityContainer();
            container.RegisterType<IContainerProvider, ContainerProvider>();
            container.RegisterType<IItemFactory, ItemFactory>(
                new InjectionConstructor(
                    new ResolvedParameter<IContainerProvider>(), 
                    Model.FieldWrapperInterfaceMap.Mappings, 
                    "Herskind.Model"
                ), 
                new InjectionProperty("SitecoreProvider", mock.Object));

            // Act
            var itemFactory = container.Resolve<IItemFactory>();
            var contextItem = itemFactory.GetContextItem<ISampleItem>();

            // Assert
            Assert.IsNotNull(contextItem, "Context item could not be resolved as ISampleItem type");
            Assert.AreEqual(contextItem.Title.RawValue, "Title");
        }


    }
}
