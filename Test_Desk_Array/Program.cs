using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_002;

namespace Test_Desk_Array
{
    class Program
    {
        #region Show

        static void Show(IEnumerable array, string name)
        {
            Console.WriteLine(name);
            foreach (var item in array)
                Console.Write(" {0},", item);
            Console.WriteLine("\n" + new string('-', 30));
        }

        #endregion

        static void Main(string[] args)
        {
            MultiArray<int> array1 = new MultiArray<int>(3);
            for (int i = 0; i < array1.Capacity; i++)
                array1[i] = i;

            Show(array1, "Array 1");

            object[] objArray = { 1, "Two", 3.0, "Four", '\u0058' };

            MultiArray<object> array2 = new MultiArray<object>(objArray, -12);
            Show(array2, "Array 2");

            Console.WriteLine("Value of -11 in Array2 = {0}", array2[-11]);
            Console.WriteLine("Index of \"Four\" in Array2 = {0}", array2.IndexOf("Four"));
            Console.WriteLine("1 contains in Array2 = {0}", array2.Contains(1));
            Console.WriteLine(new string('-', 30));

            MultiArray<char> array3 = new MultiArray<char>(-3, 3);

            for (int i = array3.FirstIndex; i <= array3.LastIndex; i++)
                array3[i] = (char)(i + 80);

            Show(array3, "Array 3");

            array3.InsertBefore((char)88, -3);
            Console.WriteLine("First index = {0} and last index {1}", array3.FirstIndex, array3.LastIndex);

            array3.InsertAfter((char)89, 3);
            Console.WriteLine("First index = {0} and last index {1}", array3.FirstIndex, array3.LastIndex);

            array3.RemoveWithRightMove(2);
            Console.WriteLine("First index = {0} and last index {1}", array3.FirstIndex, array3.LastIndex);

            array3.RemoveWithLeftMove(1);
            Console.WriteLine("First index = {0} and last index {1}", array3.FirstIndex, array3.LastIndex);

            Show(array3, "Array 3");

            Console.ReadKey();
        }
    }
}
