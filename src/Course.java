public abstract class Course implements Learnable
{
    protected String title;
    protected String instructor;
    protected int totalLessons;
    protected double price;

    public Course(String title, String instructor, int totalLessons, double price) {
        this.title = title;
        this.instructor = instructor;
        this.totalLessons = totalLessons;
        this.price = price;
    }

    void enroll(Student student)
    {
        student.onEnrollmentSuccess(title, instructor);
    }
    void completeLesson(int lessonNumber, Student student)
    {
        if (lessonNumber <= totalLessons)
        {
            student.onLessonCompleted(title, lessonNumber, totalLessons);
            if (lessonNumber == totalLessons)
            {
                student.setCompletedCourses(student.getCompletedCourses() + 1);
                student.onCourseCompleted(title, "Certificate of " + title);
            }
        }
    }
}
