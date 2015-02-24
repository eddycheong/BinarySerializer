using BinarySerialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinarySerializer.Test.Misc {
    [TestClass]
    public class TrailingFixedLengthFieldTests : TestBase {
        public class TrailingFixLengthFieldClass
        {
            [FieldOrder(0)]
            public byte[] Data { get; set; }

            [FieldOrder(1)]
            public byte Checksum { get; set; }
        }
        
        [TestMethod]
        public void NonGreedySerializationTest()
        {
            var rawData = new byte[] {0x01, 0x02, 0x03, 0x04};

            var actual = Serializer.Deserialize<TrailingFixLengthFieldClass>(rawData);

            Assert.AreEqual(3, actual.Data.Length);
            Assert.AreEqual(0x04, actual.Checksum);
        }
    }
}
