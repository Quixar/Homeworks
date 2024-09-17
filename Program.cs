using System;

namespace student;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Student s = new Student();
        Student s2 = (Student)s.Clone();
        Student s3 = new Student();
        Student s4 = new Student();

        s.AddExam(7);
        s2.AddExam(8);
        s3.AddExam(6);

        List<Student> a = new List<Student>();

        a.Add(s);
        a.Add(s2);
        a.Add(s3);
        a.Sort();

        foreach (Student stud in a)
        {
            stud.Show();
        }

        Group g = new Group();

        g.AddStudent(s);
        g.AddStudent(s2);
        g.AddStudent(s3);
        Group g2 = (Group)g.Clone();
        g2.ShowGroup();


        // s.AddExam(12);
        // s2.AddExam(6);
        // s3.AddExam(7);

        // if(s)
        // {
        //     System.Console.WriteLine("true");
        // }

        // else
        // {
        //     System.Console.WriteLine("false");
        // }

        // if(s > s2)
        // {
        //     System.Console.WriteLine("true");
        // }

        // if(s2 < s)
        // {
        //     System.Console.WriteLine("true");
        // }

        // if(s == s2)
        // {
        //     System.Console.WriteLine("true");
        // }

        // if(s != s2)
        // {
        //     System.Console.WriteLine("true");
        // }

        // g.AddStudent(s);
        // g.AddStudent(s2);
        // g.AddStudent(s3);

        // g2.AddStudent(s);
        // g2.AddStudent(s2);
        // g2.AddStudent(s3);

        
        // if(g == g2)
        // {
        //     System.Console.WriteLine("true");
        // }
        // else
        // {
        //     System.Console.WriteLine("false");
        // }

        // g.ExpelBadStudent();
        // g.ShowGroup();
        // Group copy = new Group();
        
        // g.ShowGroup();
        // g.TransferStudent(s, copy);
        // g.ShowGroup();
        // copy.ShowGroup();System.Console.WriteLine("test");
    }
}