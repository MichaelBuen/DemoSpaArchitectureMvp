using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestFriendlyDal;

namespace Domain.Test
{
    [TestClass]
    public class TheUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (IDataStore ds = new DataStore(Common.BuildSessionFactory()))
            {
                // Arrange
                string expectedProduct = "Tesla";
                string expectedCategory = "Car";

                // Act
                var p = ds.Get<TheProduction.Product>(1);

                // Assert
                Assert.AreEqual(expectedProduct, p.ProductName);
                Assert.AreEqual(expectedCategory, p.ProductCategory.ProductCategoryName);                
            }
        }
    }
}
