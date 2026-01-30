public class DesignCourse extends Course
{
    private String designTool;

    public DesignCourse(String title, String instructor, int totalLessons, double price, String designTool) {
        super(title, instructor, totalLessons, price);
        this.designTool = designTool;
    }


    @Override
    public void learn() {
        System.out.println("learing...");
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
