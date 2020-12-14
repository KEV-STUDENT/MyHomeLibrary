using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;

namespace MyHLibFiles.Test
{
    [TestClass]
    public class HLibFileZIPTest_Exceptions
    {
        [TestMethod]
        public void HLibFileZIP_GetDiscItemsList_SYSERROR()
        {
            HLibFileZIP zip = new HLibFileZIP(@"F:\1", "SYSERROR.ZIP");
            List<HLibDiscItem> list = zip.GetDiscItemsList();
            foreach (var item in list)
            {
                Debug.WriteLine(item.FullName);
            }
            Assert.AreEqual(0, list.Count);
        }
    }
}
