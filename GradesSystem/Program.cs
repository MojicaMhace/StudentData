using System;
using System.Collections.Generic;
using GradeManagementDL;
using GradeManagemenrBL;
using ModelList;

namespace GradesSystemUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Credential> studentsDB = SqlDbData.GetList();

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("Welcome to the Grade Management System");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            Console.Write("Enter Student Name: ");
            string studentName = Console.ReadLine();
            Console.Write("Enter Course and Section: ");
            string courseSection = Console.ReadLine();

            Credential existingStudent = studentsDB.Find(student => student.StudentName == studentName && student.CourseSection == courseSection);

            if (existingStudent != null)
            {
                Console.WriteLine("Student exists. Do you want to update the record? (yes/no)");
                string updateChoice = Console.ReadLine().ToLower();

                if (updateChoice == "yes")
                {
                    UpdateStudentData(existingStudent);
                }
            }
            else
            {
                Console.WriteLine("Student does not exist. Adding a new student record.");
                AddStudentData(studentName, courseSection);
            }
        }

        private static void AddStudentData(string studentName, string courseSection)
        {
            Console.WriteLine("Enter Subject Grades");
            double datastruc = GetGrade("Data Structure");
            double algo = GetGrade("Algorithms");
            double database = GetGrade("Database Management");
            double prog = GetGrade("Programming3");
            double network = GetGrade("Data and Networking");

            double averageGrade = CalculateAverageGrade(datastruc, algo, database, prog, network);

            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("This is your Credentials");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("Student Name: " + studentName);
            Console.WriteLine("Course and Section: " + courseSection);
            Console.WriteLine("General Weighted Average: " + averageGrade);

            AcademicService academicAchievement = new AcademicService(averageGrade);
            string achievement = academicAchievement.GetAcademicAward();
            Console.WriteLine("Academic Achievement: " + achievement);
            Console.WriteLine("------------------------------------------------------------------------------------------");

            Credential newCredential = new Credential
            {
                StudentName = studentName,
                CourseSection = courseSection,
                Average = averageGrade
            };

            SqlDbData.AddData(studentName, courseSection, averageGrade);
        }

        private static double GetGrade(string subject)
        {
            Console.Write($"{subject}: ");
            return Convert.ToDouble(Console.ReadLine());
        }

        private static double CalculateAverageGrade(double datastruc, double algo, double database, double prog, double network)
        {
            CalculateGrade calculateGrade = new CalculateGrade(datastruc, algo, database, prog, network);
            return calculateGrade.GetAverageGrade();
        }

        private static void UpdateStudentData(Credential existStudentInfo)
        {
            Console.WriteLine("Enter new Subject Grades to update");
            double datastruc = GetGrade("Data Structure");
            double algo = GetGrade("Algorithms");
            double database = GetGrade("Database Management");
            double prog = GetGrade("Programming3");
            double network = GetGrade("Data and Networking");

            double averageGrade = CalculateAverageGrade(datastruc, algo, database, prog, network);

            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("This is your updated Credentials");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("Student Name: " + existStudentInfo.StudentName);
            Console.WriteLine("Course and Section: " + existStudentInfo.CourseSection);
            Console.WriteLine("General Weighted Average: " + averageGrade);

            AcademicService academicAchievement = new AcademicService(averageGrade);
            string achievement = academicAchievement.GetAcademicAward();
            Console.WriteLine("Academic Achievement: " + achievement);
            Console.WriteLine("------------------------------------------------------------------------------------------");

            existStudentInfo.Average = averageGrade;
            //SqlDbData.UpdateData(existStudentInfo);
        }
    }
}
