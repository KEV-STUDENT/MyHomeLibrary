using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MyHLibFiles.Test
{
    [TestClass]
    public class HLibFactoryTest
    {
        private string path = @"F:\1\";
        private string name = "test";
        private HLibDiscItem item;

        [TestInitialize]
        public void Setup()
        {
            item = HLibFactory.GetDiskItem(path, name);
        }

        [TestMethod]
        public void HLibFactory_GetDiscItem_HLibDiscItem()
        {
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void HLibFactory_GetDiscItem_HLibDirectory()
        {
            Assert.IsInstanceOfType(item, typeof(HLibDirectory));
        }
    }
}
