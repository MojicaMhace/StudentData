using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ModelList;

namespace GradeManagementDL
{
    public class SqlDbData
    {
        static string connectionString = "Data Source=DESKTOP-4VKSJ0D\\SQLEXPRESS; Initial Catalog=StudentData; Integrated Security=True;";
        static SqlConnection sqlConnection = new SqlConnection(connectionString);

        public static void Connect()
        {
            sqlConnection.Open();
        }

        public static List<Credential> GetList()
        {
            string selectStatement = "SELECT * FROM Studenttbl";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            List<Credential> credentials = new List<Credential>();

            while (reader.Read())
            {
                string StudentName = reader["Student_Name"].ToString();
                string CourseSection = reader["Course_Section"].ToString();
                double Average = Convert.ToDouble(reader["Average_Grade"]);

                Credential readUser = new Credential();
                readUser.StudentName = StudentName;
                readUser.CourseSection = CourseSection;
                readUser.Average = Average; 

                credentials.Add(readUser);
                //credentials.Add(new Credential
                //{
                //    StudentName = reader["Student_Name"].ToString(),
                //    CourseSection = reader["Course_Section"].ToString(),
                //    Average = Convert.ToDouble(reader["Average_Grade"])
                //});
            }

            sqlConnection.Close();
            return credentials;
        }

        public static void AddData(string studentName, string courseSection, double average)
        {
            string insertStatement = "INSERT INTO Studenttbl (Student_Name, Course_Section, Average_Grade) VALUES (@StudentName, @CourseSection, @AverageGrade)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);
            insertCommand.Parameters.AddWithValue("@StudentName", studentName);
            insertCommand.Parameters.AddWithValue("@CourseSection", courseSection);
            insertCommand.Parameters.AddWithValue("@AverageGrade", average);
            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static void DeleteData(string studentName, string courseSection, double average)
        {
            string deleteStatement = "DELETE FROM Studenttbl WHERE Student_Name = @StudentName AND Course_Section = @CourseSection AND Average_Grade = @AverageGrade";

                SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
                deleteCommand.Parameters.AddWithValue("@StudentName", studentName);
                deleteCommand.Parameters.AddWithValue("@CourseSection", courseSection);
                deleteCommand.Parameters.AddWithValue("@AverageGrade", average);

                sqlConnection.Open();
                deleteCommand.ExecuteNonQuery();
                sqlConnection.Close();
        }

        public static void UpdateData(string studentName, string courseSection, double average)
        {
            string updateStatement = "UPDATE Studenttbl SET Average_Grade = @AverageGrade WHERE Student_Name = @StudentName AND Course_Section = @CourseSection";
                SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
                updateCommand.Parameters.AddWithValue("@StudentName", studentName);
                updateCommand.Parameters.AddWithValue("@CourseSection", courseSection);
                updateCommand.Parameters.AddWithValue("@AverageGrade", average);
                sqlConnection.Open();
                updateCommand.ExecuteNonQuery();
                sqlConnection.Close();

        }

    }
}
