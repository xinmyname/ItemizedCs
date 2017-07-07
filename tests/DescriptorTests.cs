using Microsoft.VisualStudio.TestTools.UnitTesting;
using Itemize.Models;

namespace tests
{
    [TestClass]
    public class DescriptorInitializerTests {
        
        [TestMethod]
        public void testThatADescriptorIsMadeGivenNothing() {
            var d = new Descriptor();
            Assert.AreEqual(d.ToString().Length, 0);
        }
        
        [TestMethod]
        public void testThatADescriptorIsMadeGivenSingleIndexAsAString() {
            var d = new Descriptor("1");
            Assert.AreEqual(d.ToString(), "1");
        }
        
        [TestMethod]
        public void testThatADescriptorIsMadeGivenTwoIndicesAsAString() {
            var d = new Descriptor("1-2");
            Assert.AreEqual(d.ToString(), "1-2");
        }
        
        [TestMethod]
        public void testThatADescriptorIsMadeGivenOneIndexAndOneNullIndexAsAString() {
            var d = new Descriptor("1-?");
            Assert.AreEqual(d.ToString(), "1-?");
        }
        
        [TestMethod]
        public void testThatADescriptorIsMadeGivenSingleIndexAsAnArray() {
            var d = new Descriptor(new[] {1});
            Assert.AreEqual(d.ToString(), "1");
        }
        
        [TestMethod]
        public void testThatADescriptorIsMadeGivenTwoIndicesAsAnArray() {
            var d = new Descriptor(new[] {1,2});
            Assert.AreEqual(d.ToString(), "1-2");
        }
        
        [TestMethod]
        public void testThatADescriptorIsMadeGivenOneIndexAndOneNullIndexAsAnArray() {
            var d = new Descriptor(new[] {1,-1});
            Assert.AreEqual(d.ToString(), "1-?");
        }
    }

    [TestClass]
    public class GivenAnEmptyDescriptor {
        
        [TestMethod]
        public void testThatAnIndexIsAppended() {
            var d = new Descriptor();
            d.Append(0);
            Assert.AreEqual(d.ToString(), "0");
        }
        
        [TestMethod]
        public void testThatANullIndexIsAppended() {
            var d = new Descriptor();
            d.Append(null);
            Assert.AreEqual(d.ToString(), "?");
        }
    }

    [TestClass]
    public class GivenADescriptorWithANullIndex {
        
        [TestMethod]
        public void testThatTheIndexIsNull() {
            var d = new Descriptor("?");
            DescriptorIterator it = d.Iterator;

            try {
                int? index = it.Next();
                Assert.IsNull(index);
            } catch {
                Assert.Fail();
            }
        }
    }

    [TestClass]
    public class GivenADescriptorWithTwoIndices {
        
        [TestMethod]
        public void testThatAnIndexIsAppended() {
            var d = new Descriptor("1-2");
            d.Append(3);
            Assert.AreEqual(d.ToString(), "1-2-3");
        }
        
        [TestMethod]
        public void testThatANullIndexIsAppended() {
            var d = new Descriptor("1-2");
            d.Append(null);
            Assert.AreEqual(d.ToString(), "1-2-?");
        }
        
        [TestMethod]
        public void testThatAnIteratorContainsTwoItems() {
            var d = new Descriptor("1-2");
            var it = d.Iterator;
            it.Next();
            it.Next();
        }
        
        [TestMethod]
        public void testThatAnIteratorDoesNotContainThreeItems() {
            var d = new Descriptor("1-2");
            var it = d.Iterator;
            it.Next();
            it.Next();
            Assert.ThrowsException<DescriptorIterator.NoMoreItemsException>(() => { it.Next(); });
        }
    }

    [TestClass]
    public class GivenTwoDescriptorsWithTheSameIndices {
        
        [TestMethod]
        public void testThatTheyAreEqual() {
            var d1 = new Descriptor("1-?-3");
            var d2 = new Descriptor(new[] {1,-1,3});
            Assert.AreEqual(d1, d2);
        }
    }

    [TestClass]
    public class GivenTwoDescriptorsWithDifferentIndices {
        
        [TestMethod]
        public void testThatTheyAreNotEqual() {
            var d1 = new Descriptor("1-?");
            var d2 = new Descriptor(new[] {2,1});
            Assert.AreNotEqual(d1, d2);
        }
    }

    [TestClass]
    public class GivenTwoDescriptorsWithDifferentLengths {
        
        [TestMethod]
        public void testThatTheyAreNotEqual() {
            var d1 = new Descriptor("1-?-3-4-5");
            var d2 = new Descriptor(new[] {1,-1,3});
            Assert.AreNotEqual(d1, d2);
        }
    }
}
