public class BusinessCourse extends Course
{
    private String category;

    public BusinessCourse(String title, String instructor, int totalLessons, double price, String category) {
        super(title, instructor, totalLessons, price);
        this.category = category;
    }


    @Override
    public void learn() {
        System.out.println("learning...");
    }

    @Override
    public int getDuration() {
        return totalLessons * 2;
    }

    @Override
    public String getLevel() {
        return "Beginner";
    }
}
