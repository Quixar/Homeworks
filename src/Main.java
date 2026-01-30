void main()
{
    Student s1 = new Student("s1", "s1");
    Student s2 = new Student("s2", "s2");
    Student s3 = new Student("s3", "s3");

    Course c1 = new ProgrammingCourses("c1", "prof", 2, 100.0, "Java");
    Course c2 = new ProgrammingCourses("c2", "prof", 2, 100.0, "Python");
    Course c3 = new DesignCourse("c3", "prof", 2, 100.0, "Figma");
    Course c4 = new BusinessCourse("c4", "prof", 2, 100.0, "Marketing");

    c1.enroll(s1);
    c2.enroll(s2);
    c3.enroll(s3);

    c1.completeLesson(1, s1);
    c1.completeLesson(2, s1);

    c2.completeLesson(1, s2);
    c2.completeLesson(2, s2);

    c3.completeLesson(1, s3);

    System.out.println(s1.getCompletedCourses());
    System.out.println(s2.getCompletedCourses());
    System.out.println(s3.getCompletedCourses());

}