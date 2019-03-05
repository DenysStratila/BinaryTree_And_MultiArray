using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Task_001.Tests
{
    [TestClass()]
    public class BinaryTreeTests
    {
        BinaryTree<int> testTree;

        [TestInitialize]
        public void TestInitialize()
        {
            testTree = new BinaryTree<int>();
        }

        #region Constructor tests

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void BinaryTreeTest_ConstructorReceivesNullArgument_ShouldThrowNullReferenceException()
        {
            BinaryTree<int> actual = new BinaryTree<int>(null);
        }

        [TestMethod()]
        public void BinaryTreeTest_ConstructorReceivesArrayOfComparableNodes_BinaryTreeHasCreated()
        {
            BinaryTree<int> actual = new BinaryTree<int>(new IComparable<int>[] { 0 });

            Assert.AreEqual(actual.Root.Value, 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ComparatorHasNotBeenFoundException))]
        public void BinaryTreeTest_ConstructorReceivesNullOntoDelegateArgument_ShouldThrowComparatorHasNotBeenFoundException()
        {
            BinaryTree<double> result = new BinaryTree<double>(new double[] { 3.14 }, null);
        }

        #endregion

        #region Add tests

        [TestMethod()]
        public void AddTest_EmptyTree_AddsRootNode()
        {
            Assert.IsNull(testTree.Root);
            testTree.Add(1);

            Assert.IsNotNull(testTree.Root);
        }

        [TestMethod()]
        public void AddTest_NodeLessThanRoot_AddsLeftNodeToRoot()
        {
            testTree.Add(2);
            testTree.Add(1);

            Assert.IsNotNull(testTree.Root.Left);
        }

        [TestMethod()]
        public void AddTest_NodeMoreThanRootOrEqualToIt_AddsRightNodeToRoot()
        {
            testTree.Add(2);
            testTree.Add(3);

            Assert.IsNotNull(testTree.Root.Right);
        }

        #endregion

        #region Event test

        [TestMethod]
        public void EventTest_AddNodeEventIsRaised_HasShowedMessage()
        {
            string actual = null;
            int value = 7;
            testTree.AddNodeEvent += delegate (string mes) { actual = mes; };

            testTree.Add(value);
            string expected = $"Node {testTree.Counter} has added to Binary Tree with value {value}";

            Debug.WriteLine(actual);
            Debug.WriteLine(expected);
            Assert.AreEqual(actual, expected);
        }

        #endregion

        #region Traversal tests

        [TestMethod()]
        public void GetEnumeratorTest_InOrderTraversal_IsInOrderTraversal()
        {
            testTree = new BinaryTree<int>(new IComparable<int>[] { 7, 5, 9, 4, 6, 8, 10 });
            string actual = "";

            foreach (var testNode in testTree)
                actual += testNode;

            Debug.WriteLine("InOrder Traversal: " + actual);
            Assert.AreEqual("45678910", actual);
        }

        [TestMethod()]
        public void PreOrderTravTest_PreOrderTraversal_IsPreOrderTraversal()
        {
            testTree = new BinaryTree<int>(new IComparable<int>[] { 7, 5, 9, 4, 6, 8, 10 });
            string actual = "";

            foreach (var testNode in testTree.PreOrderTrav)
                actual += testNode;

            Debug.WriteLine("Pre Order Traversal: " + actual);
            Assert.AreEqual("75469810", actual);
        }

        [TestMethod()]
        public void PostOrderTravTest_PostOrderTraversal_IsPostOrderTraversal()
        {
            testTree = new BinaryTree<int>(new IComparable<int>[] { 7, 5, 9, 4, 6, 8, 10 });
            string actual = "";

            foreach (var testNode in testTree.PostOrderTrav)
                actual += testNode;

            Debug.WriteLine("Post Order Traversal: " + actual);
            Assert.AreEqual("46581097", actual);
        }

        #endregion

        #region Deleting nodes tests

        [TestMethod()]
        public void RemoveTest_NodeDoesNotExist_FalseReturned()
        {
            testTree.Add(3);
            bool actual = testTree.Remove(5);

            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void RemoveTest_DeletingNodeDoesNotHaveChild_NodeIsDeleted()
        {
            testTree = new BinaryTree<int>(new IComparable<int>[] { 7, 5, 4 });

            testTree.Remove(5);

            Assert.IsTrue(testTree.Root.Left.Value == 4);
        }

        [TestMethod()]
        public void RemoveTest_DeletingNodeHasRightChild_NodeIsDeleted()
        {
            testTree = new BinaryTree<int>(new IComparable<int>[] { 8, 5, 2, 6, 7 });

            testTree.Remove(5);

            Assert.IsTrue(testTree.Root.Left.Value == 6);
            Assert.IsTrue(testTree.Root.Left.Left.Value == 2);
        }

        [TestMethod()]
        public void RemoveTest_DeletingNodeHasRightChildWhichHasLeftChild_NodeIsDeleted()
        {
            testTree = new BinaryTree<int>(new IComparable<int>[] { 7, 4, 3, 6, 5 });

            testTree.Remove(4);

            Assert.IsTrue(testTree.Root.Left.Value == 5);
            Assert.IsTrue(testTree.Root.Left.Left.Value == 3);
            Assert.IsTrue(testTree.Root.Left.Right.Value == 6);
            Assert.IsNull(testTree.Root.Left.Right.Left);
        }

        #endregion

        #region Searching nodes tests

        [TestMethod()]
        public void ContainsTest_NodeDoesNotExist_ReturnedFalse()
        {
            testTree = new BinaryTree<int>(new IComparable<int>[] { 7, 5, 9, 4, 6, 8, 10 });

            bool actual = testTree.Contains(1);

            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void ContainsTest_NodeExists_ReturnedTrue()
        {
            testTree = new BinaryTree<int>(new IComparable<int>[] { 7, 5, 9, 4, 6, 8, 10 });

            bool actual = testTree.Contains(8);

            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void ContainsTest_TreeIsEmpty_ReturnedFalse()
        {
            bool actual = testTree.Contains(1);

            Assert.IsNull(testTree.Root);
            Assert.IsFalse(actual);
        }

        #endregion
    }
}