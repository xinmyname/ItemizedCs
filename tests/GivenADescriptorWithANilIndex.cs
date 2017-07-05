using Microsoft.VisualStudio.TestTools.UnitTesting;
using Itemize.Models;

namespace tests
{
    [TestClass]
    public class GivenADescriptorWithANilIndex {

        [TestClass]
        public class WhenIndexZeroIsAppended {

            [TestMethod]
            public void TheStringIsZero() {
                var d = new Descriptor();
                d.Append(0);
                Assert.AreEqual(d.ToString(), "0");
            }
        }

        [TestClass]
        public class WhenANullIndexIsAppended {

            [TestMethod]
            public void TheStringIsQuestionMark() {
                var d = new Descriptor();
                d.Append(null);
                Assert.AreEqual(d.ToString(), "?");
            }
        }
    }
}