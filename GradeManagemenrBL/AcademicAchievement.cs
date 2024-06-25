namespace GradeManagemenrBL
{
    public class AcademicAchievementService
    {
        public double Average;

        public AcademicAchievementService(double average)
        {
            Average = average;
        }

        public string GetAcademicAward()
        {
            if (Average >= 1.00 && Average <= 1.50)
            {
                return "Congratulations! You are a President Lister!";
            }
            else if (Average > 1.50 && Average <= 1.75)
            {
                return "Congratulations! You are a Dean Lister!";
            }
            else
            {
                return "Sorry, you are not qualified to be in the President's or Dean's List.";
            }
        }
    }
}
