using System;
namespace student
{
    class Student : ICloneable, IComparable<Student>
    {
        string name;
        string surname;
        string middlename;
        string address;
        string phone;

        bool expel = false;

        DateTime birthday;

        List<int> homeworks;
        List<int> courseworks;
        List<int> exams;

        public Student() : this("Name", "Surname", "Middlename", "Bucharest",
            "+40792883327", new DateTime(2006, 07, 17))
        {

        }

        public Student(string name, string surname, string middlename)
            : this(name, surname, middlename, "Bucharest", "+40792883327", new DateTime(2006, 07, 17))
        {

        }

        public Student(string name, string surname, string middlename,
            string address, string phone, DateTime birthday)
        {
            SetName(name);
            SetSurname(surname);
            SetMiddleName(middlename);
            SetAddress(address);
            SetPhone(phone);
            SetBirthday(birthday);

            homeworks = new List<int>();
            courseworks = new List<int>();
            exams = new List<int>();
        }

        public Student(Student student)
        {
            name = student.name;
            surname = student.surname;
            middlename = student.middlename;
            address = student.address;
            phone = student.phone;
            birthday = student.birthday;

            homeworks = new List<int>(student.homeworks);
            courseworks = new List<int>(student.courseworks);
            exams = new List<int>(student.exams);

        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetSurname(string surname)
        {
            this.surname = surname;
        }

        public void SetMiddleName(string middlename)
        {
            this.middlename = middlename;
        }

        public void SetBirthday(DateTime birthday)
        {
            this.birthday = birthday;
        }

        public void SetAddress(string address)
        {
            this.address = address;
        }

        public void SetPhone(string phone)
        {
            this.phone = phone;
        }

        public string GetName()
        {
            return name;
        }

        public string GetSurname()
        {
            return surname;
        }

        public string GetMiddleName()
        {
            return middlename;
        }

        public string GetAddress()
        {
            return address;
        }

        public string GetPhone()
        {
            return phone;
        }

        public DateTime GetBirthday()
        {
            return birthday;
        }

        public void Show()
        {
            Console.WriteLine(GetSurname());
            Console.WriteLine(GetName());
            Console.WriteLine(GetMiddleName());
            Console.WriteLine(GetAddress());
            Console.WriteLine(GetPhone());
            Console.WriteLine(GetBirthday());
            Console.WriteLine("Homework grades: " + string.Join(", ", homeworks));
            Console.WriteLine("Coursework grades: " + string.Join(", ", courseworks));
            Console.WriteLine("Exam grades: " + string.Join(", ", exams));
        }

        public void AddHw(int grade)
        {
            homeworks.Add(grade);
        }

        public void AddCourse(int grade)
        {
            courseworks.Add(grade);
        }

        public void AddExam(int grade)
        {
            exams.Add(grade);
        }

        public bool PassSession()
        {
            foreach(var grade in exams)
            {
                if(grade < 7)
                {
                    return false;
                }
            }
            return true;
        }

        public double CalculateAvg()
        {
            return exams.Average();
        }

        public static bool operator true(Student student)
        {
            return !student.expel;
        }

        public static bool operator false(Student student)
        {
            return student.expel;
        }

        public static bool operator ==(Student left, Student right)
        {
            if(left.CalculateAvg() == right.CalculateAvg())
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(Student left, Student right)
        {
            return !(left == right);
        }

        public static bool operator >(Student left, Student right)
        {
            if(left.CalculateAvg() > right.CalculateAvg())
            {
                return true;
            }
            return false;
        }

        public static bool operator <(Student left, Student right)
        {
            if(left.CalculateAvg() < right.CalculateAvg())
            {
                return true;
            }
            return false;
        }

        public object Clone()
        {
            return new Student(this);
        }

        public int CompareTo(Student? obj)
        {
            if(this.CalculateAvg() > obj?.CalculateAvg()) return -1;
            if(this.CalculateAvg() < obj?.CalculateAvg()) return 1;
            return 0;
        }
    }
}