using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest_For_ICU_Config
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Alert_to_care.repo.ICUConfigTestRepository iCUConfigTestRepository = new Alert_to_care.repo.ICUConfigTestRepository();
            var icu = iCUConfigTestRepository.GetICU(0);
            Assert.AreEqual(0, icu.id);
            Assert.AreEqual(4, icu.Beds);
            Assert.AreEqual('H', icu.Layout);
        }
    }
}
