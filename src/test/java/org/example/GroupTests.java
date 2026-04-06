package org.example;
import static org.junit.jupiter.api.Assertions.*;

import java.time.LocalDate;
import java.util.List;

import org.example.Group;
import org.example.Student;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

@DisplayName("Тести для класу Group")
public class GroupTests {
    private Group group;
    private Student student1;
    private Student student2;
    private Student student3;
    private static final LocalDate VALID_DATE = LocalDate.of(2000, 1, 1);

    @BeforeEach
    void setUp() {
        group = new Group("Тестова Група");

        student1 = new Student("Тестовий Студент 1", VALID_DATE);
        student2 = new Student("Тестовий Студент 2", VALID_DATE.plusDays(1));
        student3 = new Student("Тестовий Студент 3", VALID_DATE.plusDays(2));
    }

    @Test
    @DisplayName("Перевірка створення групи з коректною назвою та порожнім списком студентів")
    void testCreateGroup() {
        assertAll("Перевірка ініціалізації групи",
                () -> assertEquals("Тестова Група", group.getGroupName(), "Назва групи має відповідати введеній"),
                () -> assertTrue(group.getStudents().isEmpty(), "Список студентів має бути порожнім"),
                () -> assertEquals(0, group.getStudentCount(), "Кількість студентів має бути 0"),
                () -> assertTrue(group.isEmpty(), "Група має бути порожньою"));
    }

    @Test
    @DisplayName("Перевірка додавання студента до групи")
    void testAddStudent() {
        group.addStudent(student1);

        assertAll("Перевірка додавання студента",
                () -> assertEquals(1, group.getStudentCount(), "Кількість студентів має бути 1"),
                () -> assertEquals(1, group.getStudents().size(), "Список студентів має містити 1 студента"),
                () -> assertEquals(student1, group.getStudents().get(0), "Доданий студент має бути в списку"),
                () -> assertFalse(group.isEmpty(), "Група не має бути порожньою"));
    }

    @Test
    @DisplayName("Перевірка методів getStudentCount і getStudents після додавання студентів")
    void testGetStudentCountAndStudents() {
        group.addStudent(student1);
        group.addStudent(student2);

        List<Student> students = group.getStudents();

        assertAll("Перевірка списку студентів",
                () -> assertEquals(2, group.getStudentCount(), "Кількість студентів має бути 2"),
                () -> assertEquals(2, students.size(), "Список студентів має містити 2 елементи"),
                () -> assertTrue(students.contains(student1), "Перший студент має бути в групі"),
                () -> assertTrue(students.contains(student2), "Другий студент має бути в групі"),
                () -> assertThrows(UnsupportedOperationException.class, () -> students.add(student3),
                        "Список студентів має бути незмінним"));
    }

    @Test
    @DisplayName("Перевірка винятків при створенні групи з некоректною назвою та додаванні null студента")
    void testInvalidGroupAndStudentInputs() {
        assertAll("Перевірка винятків для групи",
                () -> assertThrows(IllegalArgumentException.class, () -> new Group(null),
                        "null назва групи має викликати виняток"),
                () -> assertThrows(IllegalArgumentException.class, () -> new Group(""),
                        "Порожня назва групи має викликати виняток"),
                () -> assertThrows(IllegalArgumentException.class, () -> new Group("   "),
                        "Назва групи з пробілів має викликати виняток"),
                () -> assertThrows(IllegalArgumentException.class, () -> group.addStudent(null),
                        "Додавання null студента має викликати виняток"));
    }

    @Test
    @DisplayName("Перевірка видалення студента за ПІБ")
    void testRemoveStudentByName() {
        group.addStudent(student1);
        group.addStudent(student2);

        boolean removed = group.removeStudentByName("Тестовий Студент 1");

        assertAll("Перевірка видалення студента",
                () -> assertTrue(removed, "Студент має бути видалений"),
                () -> assertEquals(1, group.getStudentCount(), "Після видалення має лишитися 1 студент"),
                () -> assertFalse(group.getStudents().contains(student1), "Видалений студент не має бути в групі"),
                () -> assertTrue(group.getStudents().contains(student2), "Інший студент має лишитися в групі"));
    }

    @Test
    @DisplayName("Перевірка сортування студентів за середньою оцінкою за екзамени")
    void testGetStudentsSortedByAverageExamGrade() {
        student1.addExamGrade(10);
        student1.addExamGrade(12);

        student2.addExamGrade(8);
        student2.addExamGrade(8);

        student3.addExamGrade(12);
        student3.addExamGrade(10);

        group.addStudent(student2);
        group.addStudent(student3);
        group.addStudent(student1);

        List<Student> sortedStudents = group.getStudentsSortedByAverageExamGrade();

        assertAll("Перевірка сортування за середньою оцінкою за екзамени",
                () -> assertEquals(3, sortedStudents.size(), "У списку має бути 3 студенти"),
                () -> assertEquals(11.0, sortedStudents.get(0).getAverageExamGrade(), 0.01,
                        "Першим має бути студент з найвищою середньою оцінкою"),
                () -> assertEquals(11.0, sortedStudents.get(1).getAverageExamGrade(), 0.01,
                        "Другим має бути студент з такою ж середньою оцінкою"),
                () -> assertEquals(8.0, sortedStudents.get(2).getAverageExamGrade(), 0.01,
                        "Останнім має бути студент з найнижчою середньою оцінкою"));
    }
}