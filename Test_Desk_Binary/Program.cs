using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_001;

namespace Test_Desk_Binary
{
    class Program
    {
        static void ShowMessage(string message) { Console.WriteLine(message); } // Event handler method.

        static int Comparator(Student studentOne, Student studentTwo)
        {
            return studentOne.Mark.CompareTo(studentTwo.Mark);
        }

        static void Main(string[] args)
        {
            BinaryTree<int> classGraduates = new BinaryTree<int>();

            Student[] students = new Student[] // Creating a group of students.
            {
                new Student("Shawn", "English", "11/11/2019", 75),
                new Student("Joe", "English", "11/11/2019", 60),
                new Student("Tom", "English", "11/11/2019", 82),
                new Student("Michael", "English", "11/11/2019", 45),
                new Student("John", "English", "11/11/2019", 90)
            };

            classGraduates.AddNodeEvent += ShowMessage; // Making a subscription to the event.

            foreach (var student in students) // Adding mark of each student.
                classGraduates.Add(student.Mark);

            foreach (var student in students) // Showing all students.
                Console.WriteLine(student);

            foreach (var graduate in classGraduates) // Using an one of the enumerators: In Order(default), PreOrder or PostOrder.
                Console.WriteLine(graduate);

            Console.ReadKey();
            IComparable<int>[] nodes = { 7, 5, 9, 3, 6, 8, 10 };
            BinaryTree<int> tree = new BinaryTree<int>(nodes);
            foreach (var item in tree)
            {
                Console.WriteLine(item);
            }
            BinaryTree<Student> marks = new BinaryTree<Student>(students, (x, y) => x.Mark.CompareTo(y.Mark));

            Console.WriteLine(marks.Remove(new Student("John", "English", "11/11/2019", 90), Comparator));

            foreach (var mark in marks)
                Console.WriteLine(mark);

            Console.WriteLine(marks.Contains(new Student("Michael", "English", "11/11/2019", 45), Comparator));

            Console.ReadKey();
        }
    }
}
