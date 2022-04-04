using System;
using System.Collections.Generic;
using System.IO;


namespace Serialized
{
    class Program
    {
        public const string FilePath = "Students.json";
        public const int TuitionFee = 10000;
        static void Main(string[] args)
        {
            bool endProgram = false;
            string userInput = "";
            while (!endProgram)
            {
                Console.WriteLine(@"
                Choose an action to perform:
                 A - Add student
                 R - Remove studen
                 P - Print students list
                 TF - Print students tuition fee report
                 Q - Quit");
                userInput = Console.ReadLine().Trim().ToLower();
                switch (userInput)
                {
                    case "a":
                        AddStudent();
                        break;
                    case "r":
                        RemoveStudent();
                        break;
                    case "p":
                        printStudent();
                        break;
                    case "tf":
                        printTuitionFeeReport();
                        break;
                    case "q":
                        endProgram = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }
        public static void AddStudent()
        {
            string name = "";
            int age = 0;
            while (name == "")
            {
                Console.WriteLine("Entert student name:");
                name = Console.ReadLine().Trim();
                if (name == "") Console.WriteLine("Name field must contain at least 1 char, try agein.");
            }
            while (age == 0)
            {
                Console.WriteLine("Enter student age:");
                try
                {
                    age = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Age field accepts digit only, pleas try agein.");
                }
            }
            User addStudents = new User(name, age);
            List<User> students = new List<User>();
            foreach (User student in students)
            {
                if (student.Name.ToLower() == addStudents.Name.Trim().ToLower())
                {
                    Console.WriteLine($"Cannot add{addStudents}, student named {addStudents.Name} was already exist!");
                    return;
                }
            }
            students.Add(addStudents);
            Console.WriteLine($"Student {addStudents} added successfully.");
            //serializedAndUpdate();
        }
        public static void RemoveStudent(string studentName)
        {
            bool removed = false;
            List<User> students = Deserialize();
            foreach (User student in students)
            {
                if (student.Name.ToLower() == studentName.Trim().ToLower())
                {
                    removed = students.Remove(student);
                    break;
                }
            }
            if (removed)
                Console.WriteLine($"Student{studentName} was remove successfully.");
            else
                Console.WriteLine($"Cannot remove because student name {studentName} is not in the list.");
        }
        public static void printStudent()
        {
            List<User> students = Deserialize();
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine($"Student number [{i + 1}] : {students[i]}");
        }
        public static void printTuitionFeeReport()
        {
            List<User> students = Deserialize();
            var checkAge = (int age) => { if (age >= 25) return (TuitionFee.ToString()); else return ((TuitionFee / 10 * 9)).ToString(); };
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine($"{students[i]} need to pay {checkAge(students[i].Age)}");
        }
        public static List<User> Deserialize()
        {
            string jsonList = File.ReadAllText(FilePath);
            List<User> students = JsonSerializer.Serialize<List<User>>(jsonList);
            return students;
        }
        public static void SerializeUpdate(List<User> students)
        {
            string jsonList = JsonSerializer.Serialize(students)
                File.WriteAllText(FilePath, jsonList);
        }
    }
}
