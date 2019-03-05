using System;
using System.Collections.Generic;
using System.Collections;
 
namespace Task_002
{
    public class MultiArray<T> : IEnumerable
    {
        T[] array;

        #region Properties

        public int Capacity { get; private set; }

        public int FirstIndex { get; private set; }
        public int LastIndex { get; private set; }

        #endregion

        #region Constructors

        public MultiArray(int size)
        {
            if (size < 0)
                throw new ArgumentException("Size is less than 0");

            Capacity = size;
            FirstIndex = 0;
            LastIndex = size - 1;
            array = new T[Capacity];
        }

        public MultiArray(int from, int to)
        {
            Capacity = to - from + 1;
            FirstIndex = from;
            LastIndex = to;
            array = new T[Capacity];
        }

        public MultiArray(ICollection<T> collection, int from)
            : this(from, from + collection.Count - 1)
        {
            int counter = 0;

            foreach (var element in collection)
                array[counter++] = element;
        }

        public MultiArray(ICollection<T> collection)
            : this(collection, 0)
        {
        }

        #endregion

        #region Inserting

        public void InsertAfter(T item, int index)
        {
            int realIndex = GetInvertIndex(index);

            if (realIndex > Capacity)
                throw new IndexOutOfRangeException();

            T[] newArray = new T[Capacity + 1];

            Array.Copy(array, 0, newArray, 0, realIndex + 1);
            Array.Copy(array, realIndex, newArray, realIndex + 1, Capacity - realIndex);

            newArray[realIndex + 1] = item;
            array = newArray;
            Capacity++;
            LastIndex++;
        }

        public void InsertBefore(T item, int index)
        {
            int realIndex = GetInvertIndex(index);

            if (realIndex > Capacity)
                throw new IndexOutOfRangeException();

            T[] newArray = new T[Capacity + 1];

            Array.Copy(array, 0, newArray, 0, realIndex);
            Array.Copy(array, realIndex, newArray, realIndex + 1, Capacity - realIndex);

            newArray[realIndex] = item;
            array = newArray;
            Capacity++;
            FirstIndex--;
        }

        #endregion

        #region Removing

        public void RemoveWithRightMove(int index)
        {
            RemoveAt(index);
            LastIndex--;
        }

        public void RemoveWithLeftMove(int index)
        {
            RemoveAt(index);
            FirstIndex++;
        }

        void RemoveAt(int index)
        {
            int realIndex = GetInvertIndex(index);

            if (realIndex > Capacity)
                throw new IndexOutOfRangeException();

            T[] newArray = new T[Capacity - 1];

            Array.Copy(array, 0, newArray, 0, realIndex);
            Array.Copy(array, realIndex + 1, newArray, realIndex, Capacity - realIndex - 1);

            array = newArray;
            Capacity--;
        }

        #endregion

        #region Indexator

        public T this[int index]
        {
            get
            {
                int realIndex = GetInvertIndex(index);
                return array[realIndex];
            }
            set
            {
                int realIndex = GetInvertIndex(index);
                array[realIndex] = value;
            }
        }

        int GetInvertIndex(int index)
        {
            if (LastIndex >= index && FirstIndex <= index)
                return index - FirstIndex;
            else
                throw new IndexOutOfRangeException();
        }

        #endregion

        #region Contains & IndexOf

        public int? IndexOf(T item)
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (array[i].Equals(item))
                    return GetIndex(i);
            }
            return null;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != null;
        }

        int GetIndex(int index)
        {
            if (index >= 0 && Capacity > index)
                return index + FirstIndex;
            else
                throw new IndexOutOfRangeException();
        }

        #endregion

        #region Copy to array

        public T[] CopyToArray()
        {
            T[] newArray = new T[array.Length];

            int count = 0;
            foreach (var item in array)
                newArray[count++] = item;

            return newArray;
        }

        #endregion

        #region Enumerators

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < array.Length; i++)
                yield return array[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}