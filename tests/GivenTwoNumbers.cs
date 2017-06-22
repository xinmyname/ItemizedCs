using Microsoft.VisualStudio.TestTools.UnitTesting;
using Itemize.Infrastructure;

namespace tests
{
    [TestClass]
    public class GivenAPluralizer
    {
        [TestClass]
        public class ThePluralOf
        {
            [TestMethod]
            public void CatIsCats()
            {
                Assert.AreEqual("cats", "cat".Pluralize());
            }
        }
    }
}
