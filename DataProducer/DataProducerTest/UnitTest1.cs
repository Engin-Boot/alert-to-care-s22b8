using System;
using Xunit;
using DataProducer;

namespace DataProducerTest
{
    public class UnitTest1
    {

        [Fact]
        public void WhenReadDataAndPostIsCalledTenResponsesAreReturned()
        {
            DataReader dataReader = new DataReader();
            var lr = dataReader.ReadDataAndPost();
            Assert.True(lr[0].IsSuccessful == true);
            Assert.True(lr[9].IsSuccessful == true);
            Assert.True(lr[5].IsSuccessful == true);
        }

    }
}
