using Microsoft.VisualStudio.TestTools.UnitTesting;
using Itemize.Models;

namespace tests
{
    [TestClass]
    public class DescriptorConstructionTests
    {
        [TestMethod]
        public void WithNothing() {
            var d = new Descriptor();
            Assert.AreEqual(0, d.ToString().Length);
        }
        
        [TestMethod]
        public void WithSingleIndexAsAString() {
            var d = new Descriptor("1");
            Assert.AreEqual(d.ToString(), "1");
        }
        
        [TestMethod]
        public void WithTwoIndicesAsAString() {
            var d = new Descriptor("1-2");
            Assert.AreEqual(d.ToString(), "1-2");
        }
        
        [TestMethod]
        public void WithOneIndexAndOneNilIndexAsAString() {
            var d = new Descriptor("1-?");
            Assert.AreEqual(d.ToString(), "1-?");
        }
        
        [TestMethod]
        public void WithSingleIndexAsAnArray() {
            var d = new Descriptor(new int[]{ 1 });
            Assert.AreEqual(d.ToString(), "1");
        }
        
        [TestMethod]
        public void WithTwoIndicesAsAnArray() {
            var d = new Descriptor(new int[]{ 1, 2 });
            Assert.AreEqual(d.ToString(), "1-2");
        }
        
        [TestMethod]
        public void WithOneIndexAndOneNilIndexAsAnArray() {
            var d = new Descriptor(new int[]{ 1, -1 });
            Assert.AreEqual(d.ToString(), "1-?");
        }

    }
}
