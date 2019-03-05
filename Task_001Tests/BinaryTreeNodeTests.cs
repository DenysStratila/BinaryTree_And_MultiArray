using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task_001.Tests
{
    [TestClass()]
    public class BinaryTreeNodeTests
    {
        #region Compare to tests

        [TestMethod()]
        public void CompareToTest_UsingDefaultComparatorWithMoreValue_Returned1()
        {
            BinaryTreeNode<int> node = new BinaryTreeNode<int>(5);

            int actual = node.CompareTo(6, null);

            Assert.AreEqual(1, actual);
        }

        [TestMethod()]
        public void CompareToTest_UsingAnotherComparatorWithMoreValue_Returned1()
        {
            BinaryTreeNode<double> node = new BinaryTreeNode<double>(5.12);

            int actual = node.CompareTo(6.21, (x, y) => x.CompareTo(y));

            Assert.AreEqual(1, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ComparatorHasNotBeenFoundException))]
        public void CompareToTest_UsingAnotherComparatorWithMoreValue_ShouldThrowComparatorHasNotBeenFoundException()
        {
            BinaryTreeNode<MyClass> node = new BinaryTreeNode<MyClass>(null);

            node.CompareTo(new MyClass(), null);
        }

        class MyClass { }

        #endregion
    }
}