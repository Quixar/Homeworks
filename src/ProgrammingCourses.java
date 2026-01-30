public class ProgrammingCourses extends Course
{
    private String programmingLanguage;
    public ProgrammingCourses(String title, String instructor, int totalLessons, double price, String programmingLanguage) {
        super(title, instructor, totalLessons, price);
        this.programmingLanguage = programmingLanguage;
    }


    @Override
    public void learn() {
        System.out.println("learning " + programmingLanguage);
    }

    @Override
    public int getDuration() {
        return totalLessons * 2;
    }

    @Override
    public String getLevel() {
        if (programmingLanguage.equals("Java"))
        {
            return "Intermediate";
        }
        else if (programmingLanguage.equals("Python"))
        {
            return "Beginner";
        }
        return null;
    }
}
