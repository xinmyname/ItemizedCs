using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests
{
    [TestClass]
    public class GivenTwoNumbers
    {
        [TestClass]
        public class WhenAdded
        {
            [TestMethod]
            public void TheSumIsCorrect()
            {
                Assert.AreEqual(4, 2 + 2);
            }
        }
    }
}
