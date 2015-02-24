using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinarySerializer.Test.Order
{
    [TestClass]
    public class OrderTests : TestBase
    {
        [TestMethod]
        public void OrderTest()
        {
            var order = new OrderClass {First = 1, Second = 2};
            Roundtrip(order, new byte[] {0x1, 0x2});
        }

        [TestMethod]
        public void SingleMemberOrderShouldntThrowTest()
        {
            var order = new SingleMemberOrderClass();
            Roundtrip(order);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MultipleMembersNoOrderAttributeShouldThrowTest()
        {
            var order = new MutlipleMembersInvalidOrderClass();
            Roundtrip(order);
        }

        [TestMethod]
        public void BaseClassComesBeforeDerivedClassTest()
        {
            var order = new OrderDerivedClass { First = 1, Second = 2 };
            Roundtrip(order, new byte[] { 0x1, 0x2 });
        }

        [TestMethod]
        public void BitSizeWithFieldOrderTest()
        {
            var order = new BitSizeOrderClass {First = 0x01, Second = 0x02, Third = 0x03, Fourth = true, Fifth = false, Sixth = true};
            var expected = new Byte[] {0x09, 0x03, 0x21};
            Roundtrip(order, expected);
        }
    }
}
