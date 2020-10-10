using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Extensions;

namespace Test_For_ICUConfig
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Alert_to_care.tests.Repository.ICUConfigTestRepository iCUConfigTestRepository = new Alert_to_care.tests.Repository.ICUConfigTestRepository();
            var icu = iCUConfigTestRepository.GetICU(1);
            Assert.AreEqual(1, icu.id);
            Assert.AreEqual(10, icu.numberOfBeds);
            Assert.AreEqual('H', icu.layout);
            
        }

        [TestMethod]
        public void TestMethod2()
        {
            Alert_to_care.tests.Repository.ICUConfigTestRepository iCUConfigTestRepository = new Alert_to_care.tests.Repository.ICUConfigTestRepository();
            var response = iCUConfigTestRepository.RegisterIcu();
            Assert.AreEqual(true, response);
        }
    }
}
