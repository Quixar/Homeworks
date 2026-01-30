public interface CourseProgressListener
{
    void onEnrollmentSuccess(String courseName, String studentName);
    void onLessonCompleted(String courseName, int lessonNumber, int totalLessons);
    void onCourseCompleted(String courseName, String certificate);
    void onQuizFailed(String courseName, int score, int passingScore);
}
