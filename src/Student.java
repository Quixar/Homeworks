public class Student implements CourseProgressListener
{
    private String name;
    private String email;

    public int getCompletedCourses() {
        return completedCourses;
    }

    public void setCompletedCourses(int completedCourses) {
        this.completedCourses = completedCourses;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    private int completedCourses;

    public Student(String name, String email) {
        this.name = name;
        this.email = email;
    }

    @Override
    public void onEnrollmentSuccess(String courseName, String studentName) {
        System.out.println("Student " + studentName + " has enrolled courses of " + courseName);
    }

    @Override
    public void onLessonCompleted(String courseName, int lessonNumber, int totalLessons) {
        System.out.println("Student completed the lesson");
    }

    @Override
    public void onCourseCompleted(String courseName, String certificate) {
        System.out.println("Student completed courses of " + courseName);
    }

    @Override
    public void onQuizFailed(String courseName, int score, int passingScore) {
        System.out.println("Student failed courses of " + courseName);
    }
}
