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
            SqlDbData.Connect();
            List<Credential> students = SqlDbData.GetList();

            foreach (var student in students)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                Console.WriteLine("Welcome to the Grade Management System");
                Console.WriteLine("---------------------------------------------------s--------------------------------------");
                Console.WriteLine($"Student Name: {student.StudentName}");
                Console.WriteLine($"Course and Section: {student.CourseSection}");
                Console.WriteLine("Enter Subject Grades");

                Console.Write("Data Structure: ");
                double datastruc = Convert.ToDouble(Console.ReadLine());
                Console.Write("Algorithms: ");
                double algo = Convert.ToDouble(Console.ReadLine());
                Console.Write("Database Management: ");
                double database = Convert.ToDouble(Console.ReadLine());
                Console.Write("Programming3: ");
                double prog = Convert.ToDouble(Console.ReadLine());
                Console.Write("Data and Networking: ");
                double network = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------------------------------------------");

                SubjectList subjectList = new SubjectList
                {
                    DataStructure = datastruc,
                    Algorithm = algo,
                    DataBase = database,
                    Programming3 = prog,
                    DataComAndNetworking = network
                };

                CalculateGrade calculateGrade = new CalculateGrade(datastruc, algo, database, prog, network);
                double averageGrade = calculateGrade.GetAverageGrade();

                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("This is your Credentials");
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("Student Name: " + student.StudentName);
                Console.WriteLine("Course and Section: " + student.CourseSection);
                Console.WriteLine("General Weighted Average: " + averageGrade);

                AcademicAchievementService academicAchievement = new AcademicAchievementService(averageGrade);
                string achievement = academicAchievement.GetAcademicAward();
                Console.WriteLine("Academic Achievement: " + achievement);
                Console.WriteLine("------------------------------------------------------------------------------------------");

                Credential newCredential = new Credential
                {
                    StudentName = student.StudentName,
                    CourseSection = student.CourseSection
                };

                SqlDbData.AddData(newCredential);
            }
        }
    }
}
