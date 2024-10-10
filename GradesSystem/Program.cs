using System;
using System.Collections.Generic;
using GradeManagementDL;
using ModelList;
using GradeManagemenrBL;
using GradeManagementBL;

namespace GradesSystemUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Credential> studentsDB = SqlDbData.GetList();
            StudentService studentService = new StudentService();

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("Welcome to the Grade Management System");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            Console.Write("Enter Student Name: ");
            string studentName = Console.ReadLine();
            Console.Write("Enter Course and Section: ");
            string courseSection = Console.ReadLine();


            Console.WriteLine($"Student: {studentName} in Section: {courseSection}");

            Credential existingStudent = studentsDB.Find(student => student.StudentName == studentName && student.CourseSection == courseSection);

            if (existingStudent != null)
            {
                double averageGrade = 0;
                double updateAverage = 0;

                Console.WriteLine("Student exists. Do you want to update or delete the record? (1-update:2-delete:3-no)");
                string choice = Console.ReadLine().ToLower();

                if (choice == "1")
                {
                    averageGrade = GetGradesAndCalculateAverage();  
                    Console.WriteLine("------------------------------------------------------------------------------------------");
                    Console.WriteLine("This is your updated Credentials");
                    Console.WriteLine("------------------------------------------------------------------------------------------");
                    Console.WriteLine($"Student Name: {existingStudent.StudentName}");
                    Console.WriteLine($"Course and Section: {existingStudent.CourseSection}");
                    Console.WriteLine($"General Weighted Average: {averageGrade}");

                    AcademicService academicAchievement = new AcademicService(averageGrade);
                    string achievement = academicAchievement.GetAcademicAward();
                    Console.WriteLine($"Academic Achievement: {achievement}");
                    Console.WriteLine("------------------------------------------------------------------------------------------");

                    studentService.UpdateStudent(existingStudent.StudentName, existingStudent.CourseSection, averageGrade);
                    Email.UpdateEmail(studentName, courseSection, updateAverage);
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Are you sure you want to delete this student record? (yes or no)");
                    string deleteconfirm = Console.ReadLine().ToLower();

                    if (deleteconfirm == "yes")
                    {
                        if (existingStudent.Average != null)
                        {
                            studentService.DeleteStudent(existingStudent.StudentName, existingStudent.CourseSection, existingStudent.Average);
                            Console.WriteLine("Student record deleted successfully.");
                            string studentEmail = "mhacemojica04@gmail.com";
                            Email.deleteEmail(studentName, studentEmail);
                        }
                        else
                        {
                            Console.WriteLine("Delete Unsuccessful to the unkwown to the unknownnnn reason emeee.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Delete error.");
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Please wait for the email.");

                    averageGrade = GetGradesAndCalculateAverage();
                    string studentEmail = "mhacemojica04@gmail.com";
                    Email.SendEmail(existingStudent.StudentName, studentEmail, averageGrade);  
                }
            }
            else
            {
                Console.WriteLine("Student not found. Adding a new student record.");
                double averageGrade = GetGradesAndCalculateAverage();

                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("This is your Credentials");
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine($"Student Name: {studentName}");
                Console.WriteLine($"Course and Section: {courseSection}");
                Console.WriteLine($"General Weighted Average: {averageGrade}");

                AcademicService academicAchievement = new AcademicService(averageGrade);
                string achievement = academicAchievement.GetAcademicAward();
                Console.WriteLine($"Academic Achievement: {achievement}");
                Console.WriteLine("------------------------------------------------------------------------------------------");

                studentService.AddStudent(studentName, courseSection, averageGrade);

                string studentEmail = "mhacemojica04@gmail.com";
                Email.AddEmail(studentName, courseSection, studentEmail, averageGrade);
            }
        }
        private static double GetGrade(string subject)
        {
            Console.Write($"{subject}: ");
            return Convert.ToDouble(Console.ReadLine());
        }
        private static double GetGradesAndCalculateAverage()
        {
            Console.WriteLine("Enter Subject Grades");
            double datastruc = GetGrade("Data Structure");
            double algo = GetGrade("Algorithms");
            double database = GetGrade("Database Management");
            double prog = GetGrade("Programming 3");
            double network = GetGrade("Data and Networking");

            StudentService studentService = new StudentService();
            return studentService.CalculateAverageGrade(datastruc, algo, database, prog, network);
        }

    }
}
