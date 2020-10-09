using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_For_ICUConfig
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Alert_to_care.tests.Repository.ICUConfigTestRepository iCUConfigTestRepository = new Alert_to_care.tests.Repository.ICUConfigTestRepository();
            var icu = iCUConfigTestRepository.GetICU(0);
            Assert.AreEqual(0, icu.id);
            Assert.AreEqual(4, icu.numberOfBeds);
            Assert.AreEqual('H', icu.layout);
        }
    }
}
