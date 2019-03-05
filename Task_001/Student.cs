using System;

namespace Task_001
{
    public class Student
    {

        #region Properties

        public string Name { get; private set; }
        public string Test { get; private set; }
        public DateTime TestDate { get; private set; }
        public int Mark { get; private set; }

        #endregion

        public Student(string name, string test, string testDate, int mark)
        {
            Name = name;
            Test = test;
            TestDate = DateTime.Parse(testDate);

            if (mark >= 0 && mark <= 100) Mark = mark;
            else throw new MarkIsOutOfRangeException("Mark is out of range!");
        }

        public override string ToString()
        {
            return string.Format("Student: {0}, Test: {1}, Test date: {2}, Graduate: {3} ",
                Name, Test, TestDate, Mark);
        }
    }
}
