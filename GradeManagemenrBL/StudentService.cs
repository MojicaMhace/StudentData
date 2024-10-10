using ModelList;
using GradeManagementDL;
using System.Collections.Generic;

namespace GradeManagementBL
{
    public class StudentService
    {
        public double CalculateAverageGrade(double datastruc, double algo, double database, double prog, double network)
        {
            CalculateGrade calculateGrade = new CalculateGrade(datastruc, algo, database, prog, network);
            return calculateGrade.GetAverageGrade();
        }

        public void AddStudent(string studentName, string courseSection, double averageGrade)
        {
            SqlDbData.AddData(studentName, courseSection, averageGrade);
        }

        public void UpdateStudent(string studentName, string courseSection, double averageGrade)
        {
            SqlDbData.UpdateData(studentName, courseSection, averageGrade);
        }

        public void DeleteStudent(string studentName, string courseSection, double average)
        {
            SqlDbData.DeleteData(studentName, courseSection, average);
        }

        public List<Credential> GetStudents()
        {
            return SqlDbData.GetList(); // Get student records from the database
        }
    }
}
