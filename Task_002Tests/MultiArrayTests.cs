using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task_002.Tests
{
    [TestClass()]
    public class MultiArrayTests
    {
        readonly int[] intArray = new int[] { 0, 1, 2 };

        #region Constructor tests

        [TestMethod()]
        public void MultiArrayTest_SetSizeLessThan0_ShouldThrowArgumentException()
        {
            try
            {
                MultiArray<int> array = new MultiArray<int>(-1);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "Size is less than 0");
                return;
            }
            Assert.Fail("No exception was thrown!");
        }

        [TestMethod()]
        public void MultiArrayTest_SetSizeMoreThan0_ShouldCreateDefaultArray()
        {
            MultiArray<int> array = new MultiArray<int>(3);

            Assert.AreEqual(3, array.Capacity);
        }

        [TestMethod()]
        public void MultiArrayTest_SetConfinesFromMinus1TOPlus1Indexes_FirstIndexIsMinus1()
        {
            MultiArray<double> array = new MultiArray<double>(-1, 1);

            Assert.AreEqual(-1, array.FirstIndex);
        }

        [TestMethod()]
        public void MultiArrayTest_SetConfinesFromMinus1TOPlus1Indexes_LastIndexIsPlus1()
        {
            MultiArray<double> array = new MultiArray<double>(-1, 1);

            Assert.AreEqual(1, array.LastIndex);
        }


        [TestMethod()]
        public void MultiArrayTest_SetConfinesFromMinus1TOPlus1Indexes_MultiArrayIsCreated()
        {
            MultiArray<double> array = new MultiArray<double>(-1, 1);

            Assert.AreEqual(3, array.Capacity);
        }

        [TestMethod()]
        public void MultiArrayTest_PassCollectionFromMinus1Index_MultiArrayIsCreated()
        {
            byte[] bytes = { 0, 1, 2, 3, 4 };

            MultiArray<byte> array = new MultiArray<byte>(bytes, -1);

            Assert.AreEqual(5, array.Capacity);
        }

        [TestMethod()]
        public void MultiArrayTest_PassCollectionFromMinus1Index_FirstIndexIsMinus1()
        {
            byte[] bytes = { 0, 1, 2, 3, 4 };

            MultiArray<byte> array = new MultiArray<byte>(bytes, -1);

            Assert.AreEqual(-1, array.FirstIndex);
        }

        [TestMethod()]
        public void MultiArrayTest_PassCollectionFromMinus1Index_LastIndexIsPlus3()
        {
            byte[] bytes = { 0, 1, 2, 3, 4 };

            MultiArray<byte> array = new MultiArray<byte>(bytes, -1);

            Assert.AreEqual(3, array.LastIndex);
        }

        [TestMethod()]
        public void MultiArrayTest_PassCollectionWithoutStartingIndex_MultiArrayIsCreated()
        {
            byte[] bytes = { 0, 1, 2, 3, 4 };

            MultiArray<byte> array = new MultiArray<byte>(bytes);

            Assert.AreEqual(5, array.Capacity);
        }

        [TestMethod()]
        public void MultiArrayTest_PassCollectionWithoutStartingIndex_FirstIndexIsZero()
        {
            byte[] bytes = { 0, 1, 2, 3, 4 };

            MultiArray<byte> array = new MultiArray<byte>(bytes);

            Assert.AreEqual(0, array.FirstIndex);
        }

        [TestMethod()]
        public void MultiArrayTest_PassCollectionWithoutStartingIndex_LastIndexIsPlus4()
        {
            byte[] bytes = { 0, 1, 2, 3, 4 };

            MultiArray<byte> array = new MultiArray<byte>(bytes);

            Assert.AreEqual(4, array.LastIndex);
        }

        #endregion

        #region Indexator tests

        [TestMethod]
        public void IndexatorTest_SetValue_IsSet()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -1);

            int value = array[-1];

            Assert.AreEqual(0, array[-1]);
        }

        [TestMethod]
        public void IndexatorTest_GetValue_AndIsGot()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -1);

            array[-1] = 10;

            Assert.AreEqual(10, array[-1]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexatorTest_SetValueValueIntoIndexWhichIsOutOfRange_ShouldThrowIndexOutOfRangeException()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -1);

            array[-5] = 10;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexatorTest_GetValueValueIntoIndexWhichIsOutOfRange_ShouldThrowIndexOutOfRangeException()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -1);

            int value = array[-5];
        }

        #endregion

        #region Insert tests

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InsertAfterTest_InsertIndexWhichIsOutOfRange_ShouldThrowIndexOutOfRangeException()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.InsertAfter(10, 10);
        }

        [TestMethod()]
        public void InsertAfterTest_InsertAfterMinus2Index_IsInserted()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.InsertAfter(3, -2);

            Assert.AreEqual(3, array[-1]);
        }

        [TestMethod()]
        public void InsertAfterTest_InsertAfterMinus2Index_FirstIndexIsMinus2()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.InsertAfter(3, -2);

            Assert.AreEqual(-2, array.FirstIndex);
        }

        [TestMethod()]
        public void InsertAfterTest_InsertAfterMinus2Index_LastIndexIsPlusOne()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.InsertAfter(3, -2);

            Assert.AreEqual(1, array.LastIndex);
        }

        [TestMethod()]
        public void InsertAfterTest_InsertAfterMinus2Index_CapacityRaisedOnOne()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.InsertAfter(3, -2);

            Assert.AreEqual(4, array.Capacity);
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InsertBeforeTest_InsertIndexWhichIsOutOfRange_ShouldThrowIndexOutOfRangeException()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.InsertBefore(10, 10);
        }

        [TestMethod()]
        public void InsertBeforeTest_InsertAfterBefore2Index_IsInserted()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.InsertBefore(3, -2);

            Assert.AreEqual(3, array[-3]);
        }

        [TestMethod()]
        public void InsertBeforeTest_InsertAfterBefore2Index_FirstIndexIsMinus3()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.InsertBefore(3, -2);

            Assert.AreEqual(-3, array.FirstIndex);
        }

        [TestMethod()]
        public void InsertBeforeTest_InsertAfterBefore2Index_LastIndexIsZero()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.InsertBefore(3, -2);

            Assert.AreEqual(0, array.LastIndex);
        }

        [TestMethod()]
        public void InsertBeforeTest_InsertAfterBefore2Index_CapacityRaisedOnOne()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.InsertBefore(3, -2);

            Assert.AreEqual(4, array.Capacity);
        }

        #endregion}

        #region Remove tests

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void RemoveWithRightMoveTest_RemoveIndexWhichIsOutOfRange_ShouldThrowIndexOutOfRangeException()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.RemoveWithRightMove(10);
        }

        [TestMethod()]
        public void RemoveWithRightMoveTest_RemoveMinus1IndexWithRightMove_IsRemoved()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.RemoveWithRightMove(-1);

            Assert.IsFalse(array.Contains(1));
        }

        [TestMethod()]
        public void RemoveWithRightMoveTest_RemoveMinus1IndexWithRightMove_FirstIndexIsMinus2()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.RemoveWithRightMove(-1);

            Assert.AreEqual(-2, array.FirstIndex);
        }

        [TestMethod()]
        public void RemoveWithRightMoveTest_RemoveMinus1IndexWithRightMove_LastIndexIsMinus1()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.RemoveWithRightMove(-1);

            Assert.AreEqual(-1, array.LastIndex);
        }

        [TestMethod()]
        public void RemoveWithRightMoveTest_RemoveMinus1IndexWithRightMove_CapacityLoweredOn1()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.RemoveWithRightMove(-1);

            Assert.AreEqual(2, array.Capacity);
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void RemoveWithLeftMoveTest_RemoveIndexWhichIsOutOfRange_ShouldThrowIndexOutOfRangeException()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.RemoveWithLeftMove(10);
        }

        [TestMethod()]
        public void RemoveWithLefttMoveTest_RemoveMinus1IndexWithLeftMove_IsRemoved()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.RemoveWithLeftMove(-1);

            Assert.IsFalse(array.Contains(1));
        }


        [TestMethod()]
        public void RemoveWithLefttMoveTest_RemoveMinus1IndexWithLeftMove_FirstIndexIsMinus1()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.RemoveWithLeftMove(-1);

            Assert.AreEqual(-1, array.FirstIndex);
        }


        [TestMethod()]
        public void RemoveWithLefttMoveTest_RemoveMinus1IndexWithLeftMove_LastIndexIsZero()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.RemoveWithLeftMove(-1);

            Assert.AreEqual(0, array.LastIndex);
        }


        [TestMethod()]
        public void RemoveWithLefttMoveTest_RemoveMinus1IndexWithLeftMove_CapacityLoweredOn1()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            array.RemoveWithLeftMove(-1);

            Assert.AreEqual(2, array.Capacity);
        }

        #endregion

        #region Contains & IndexOf tests

        [TestMethod()]
        public void IndexOfTest_FindingIndexOf1_Minus1Returned()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            int? actual = array.IndexOf(1);

            Assert.AreEqual(-1, actual);
        }

        [TestMethod()]
        public void IndexOfTest_FindingIndexOf3_NullReturned()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            int? actual = array.IndexOf(3);

            Assert.AreEqual(null, actual);
        }

        [TestMethod()]
        public void ContainsTest_ContainsValue1_TrueReturned()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            bool actual = array.Contains(1);

            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void ContainsTest_ContainsValue10_FalseReturned()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            bool actual = array.Contains(10);

            Assert.IsFalse(actual);
        }

        #endregion

        #region Copy to array test

        [TestMethod]
        public void CopyToArrayTest_CopyExistingMultiArrayToArray_ArrayReturned()
        {
            MultiArray<int> array = new MultiArray<int>(intArray, -2);

            int[] actualArray = array.CopyToArray();
            int[] expectedArray = { 0, 1, 2 };

            CollectionAssert.AreEqual(expectedArray, actualArray);
        }

        #endregion
    }
}