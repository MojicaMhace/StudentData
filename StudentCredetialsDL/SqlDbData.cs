using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ModelList;

namespace GradeManagementDL
{
    public class SqlDbData
    {
        static string connectionString = "Data Source=DESKTOP-4VKSJ0D\\SQLEXPRESS; Initial Catalog= StudentData; Integrated Security=True;";

        public static void Connect()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
            }
        }
        public static List<Credential> GetList()
        {
            string selectStatement = "SELECT * FROM Studenttbl";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            List<Credential> readUser = new List<Credential>();

            while (reader.Read())
            {
                string studentName = reader["Student_Name"].ToString();
                string courseSection = reader["Course_Section"].ToString();
                double averageGrade = Convert.ToDouble(reader["Average_Grade"]);

                Credential readCredential = new Credential
                {
                    StudentName = studentName,
                    CourseSection = courseSection,
                    Average = averageGrade
                };

                readUser.Add(readCredential);
            }

            sqlConnection.Close();
            return readUser;
        }

        public static void AddData(Credential credential)
        {
            string insertStatement = "INSERT INTO Studenttbl (Student_Name, Course_Section, Average_Grade) VALUES (@StudentName, @CourseSection, @AverageGrade)";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand info = new SqlCommand(insertStatement, sqlConnection);

            info.Parameters.AddWithValue("@StudentName", credential.StudentName);
            info.Parameters.AddWithValue("@CourseSection", credential.CourseSection);
            info.Parameters.AddWithValue("@AverageGrade", credential.Average);
            sqlConnection.Open();
            info.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public static void DeleteData(Credential credential)
        {
            string deleteStatement = "DELETE FROM tblStudent WHERE Student_Name = @StudentName AND Course_Section = @CourseSection AND Average_Grade = @AverageGrade";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);

            deleteCommand.Parameters.AddWithValue("@StudentName", credential.StudentName);
            deleteCommand.Parameters.AddWithValue("@CourseSection", credential.CourseSection);
            deleteCommand.Parameters.AddWithValue("@AverageGrade", credential.Average);

            sqlConnection.Open();
            deleteCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
}
}
