using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CsharpBasic.Basic
{
    class Student
    {
        public int Score { set; get; }

        public Student(int score)
        {
            this.Score = score;
        }
    }

    class Teacher
    {
        public string Name { set; get; }
        public List<Student> Students { set; get; }

        public Teacher(string name, List<Student> students)
        {
            this.Name = name;
            this.Students = students;
        }
    }

    class SelectManyTest : CsharpBasic.Test.ITest
    {
        public void Test()
        {
            List<Teacher> teachers = new List<Teacher>
            {
                new Teacher("A", new List<Student>{ new Student(100), new Student(40), new Student(30) }),
                new Teacher("b",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                new Teacher("c",new List<Student>{ new Student(100),new Student(90),new Student(40) }),
                new Teacher("d",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                new Teacher("e",new List<Student>{ new Student(100),new Student(90),new Student(50) }),
                new Teacher("f",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                new Teacher("g",new List<Student>{ new Student(100),new Student(90),new Student(60) })
            };


            var students1 = from t in teachers
                            from s in t.Students
                            where s.Score < 60
                            select s;
            Console.WriteLine($"students1");
            students1.ToList().ForEach(t => Console.WriteLine($"{t.Score}"));

            var students2 = teachers.SelectMany(t => t.Students).Where(s => s.Score < 60);
            Console.WriteLine($"students2");
            students2.ToList().ForEach(t => Console.WriteLine($"{t.Score}"));

            var students3 = teachers.SelectMany(t => t.Students, (t, s) => new { t.Name, s.Score }).Where(t => t.Score < 60);
            Console.WriteLine($"students3");
            students3.ToList().ForEach(t => Console.WriteLine($"Teacher:{t.Name}, student:{t.Score}"));

            //error
            var students4 = teachers.Select(t => t.Students).Where(t => t.Count > 0);
            Console.WriteLine($"students4");
            students4.ToList().ForEach(t => Console.WriteLine($"{t.Select(c => c.Score)}"));

            //Another
            var scores = teachers.SelectMany((t, i) => t.Students.Select(s => s.Score)).OrderByDescending(t => t).ToList();
            scores.ForEach(s => Console.WriteLine(s));

            var records = teachers.SelectMany(t => t.Students).Select(Process);

            var records2 = teachers.SelectMany(t => t.Students).SelectMany(ProcessMany);
        }

        private string Process(Student stu, int i)
        {
            return $"Name{i},score:{stu.Score}";
        }

        private IEnumerable<string> ProcessMany(Student stu, int i)
        {
            yield return $"Name{i},score:{stu.Score}";
        }
    }
    
}
