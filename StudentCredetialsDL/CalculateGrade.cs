using System;



namespace GradeManagementDL
{
    public class CalculateGrade
    {
        public double DataStructure;
        public double Algorithm;
        public double DataBase;
        public double Programming3;
        public double DataComAndNetworking;

        public CalculateGrade(double dataStructure, double algorithm, double dataBase, double programming3, double dataComAndNetworking)
        {
            DataStructure = dataStructure;
            Algorithm = algorithm;
            DataBase = dataBase;
            Programming3 = programming3;
            DataComAndNetworking = dataComAndNetworking;
        }

        public double GetAverageGrade()
        {
            return (DataStructure + Algorithm + DataBase + Programming3 + DataComAndNetworking) / 5.0;
        }
    }
}
