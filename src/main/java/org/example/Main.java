package org.example;

import java.sql.*;
import java.util.Properties;
import java.util.Scanner;

public class Main {
    // конфіденційні дані краще зберігати у файлі конфігурації
    private static final String URL = "jdbc:oracle:thin:@localhost:1521:HOMEDB1";
    private static final String USER = "SYS";
    private static final String PASSWORD = "Admin#DB1";

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        Connection conn = null;
        Statement stmt = null;
        ResultSet rs = null;

        try {
            // встановлення з'єднання з базою даних
            conn = connectToDatabase();
            stmt = conn.createStatement();

            // перевірка існування таблиці та її видалення, якщо вона існує
//            if (tableExists(stmt, "CONTACTS")) {
//                dropTable(stmt, "CONTACTS");
//            }
//
//            createTable(stmt);
            while (true)
            {
                System.out.println("1. Add Contact");
                System.out.println("2. Remove Contact");
                System.out.println("3. Update Contact");
                System.out.println("4. View All Contacts");
                System.out.println("5. Exit");
                System.out.println("Enter your choice: ");
                int choice = sc.nextInt();
                sc.nextLine();
                switch (choice) {
                    case 1:
                        addContact(stmt, sc);
                        break;
                    case 2:
                        removeContact(stmt, sc);
                        break;
                    case 3:
                        updateContact(stmt, sc);
                        break;
                    case 4:
                        viewAllContacts(stmt);
                        break;
                    case 5:
                        System.out.println("Exiting...");
                        return;
                    default:
                        System.out.println("Invalid choice. Please try again.");
                }
            }

        } catch (SQLException | ClassNotFoundException e) {
            e.printStackTrace();
        } finally {
            // закриття ресурсів для уникнення витоку пам'яті
            closeResources(rs, stmt, conn);
        }
    }

    // метод для встановлення з'єднання з базою даних
    private static Connection connectToDatabase() throws ClassNotFoundException, SQLException {
        // завантаження драйвера oracle jdbc
        Class.forName("oracle.jdbc.driver.OracleDriver");
        Properties props = new Properties();
        props.put("user", USER);
        props.put("password", PASSWORD);
        // встановлення ролі sysdba для доступу до адміністративних функцій
        props.put("internal_logon", "SYSDBA");
        return DriverManager.getConnection(URL, props);
    }

    // перевірка, чи існує таблиця в базі даних
    private static boolean tableExists(Statement stmt, String tableName) throws SQLException {
        // запит для перевірки наявності таблиці в базі даних
        String checkTableSQL = "SELECT COUNT(*) FROM all_tables WHERE table_name = '" + tableName + "'";
        try (ResultSet rs = stmt.executeQuery(checkTableSQL)) {
            rs.next();
            return rs.getInt(1) > 0;
        }
    }

    // видалення таблиці з бази даних
    private static void dropTable(Statement stmt, String tableName) throws SQLException {
        String dropTableSQL = "DROP TABLE " + tableName;
        stmt.executeUpdate(dropTableSQL);
        System.out.println("Таблицю " + tableName + " видалено");
    }

    // створення нової таблиці demo
    private static void createTable(Statement stmt) throws SQLException {
        // створення таблиці з полями id, productname, customername тощо
        String createTableSQL = "CREATE TABLE CONTACTS ("
                + "ID NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY, "
                + "FIRST_NAME VARCHAR2(50) NOT NULL, "
                + "LAST_NAME VARCHAR2(50) NOT NULL, "
                + "PHONE_NUMBER VARCHAR2(20) NOT NULL)";
        stmt.executeUpdate(createTableSQL);
        System.out.println("Таблицю CONTACTS створено");
    }

    private static void addContact(Statement stmt, Scanner scanner) throws SQLException
    {
        System.out.println("Enter your name: ");
        String firstName = scanner.nextLine();

        System.out.println("Enter your last name: ");
        String lastName = scanner.nextLine();

        System.out.println("Enter your phone number: ");
        String phoneNumber = scanner.nextLine();

        String insertContactSQL = "INSERT INTO CONTACTS (FIRST_NAME, LAST_NAME, PHONE_NUMBER) VALUES "
                + "('" + firstName + "', '" + lastName + "', '" + phoneNumber + "')";
        stmt.executeUpdate(insertContactSQL);
        System.out.println("Контакт додано");
    }

    private static void removeContact(Statement stmt, Scanner scanner) throws SQLException
    {
        System.out.println("Enter the ID of the contact you want to remove: ");
        int contactId = scanner.nextInt();

        if (!contactExists(stmt, contactId)) {
            System.out.println("Contact with ID " + contactId + " does not exist.");
            return;
        }

        String deleteContactSQL = "DELETE FROM CONTACTS WHERE ID = " + contactId;
        stmt.executeUpdate(deleteContactSQL);
        System.out.println("Contact with ID " + contactId + " removed.");
    }

    private static void updateContact(Statement stmt, Scanner scanner) throws SQLException
    {
        System.out.println("Enter the ID of the contact you want to update: ");
        int contactId = scanner.nextInt();
        scanner.nextLine();
        if (!contactExists(stmt, contactId)) {
            System.out.println("Contact with ID " + contactId + " does not exist.");
            return;
        }

        System.out.println("Enter the new first name: ");
        String firstName = scanner.nextLine();

        System.out.println("Enter the new last name: ");
        String lastName = scanner.nextLine();

        System.out.println("Enter the new phone number: ");
        String phoneNumber = scanner.nextLine();

        String updateContactSQL = "UPDATE CONTACTS SET FIRST_NAME = '" + firstName + "', LAST_NAME = '" + lastName + "', PHONE_NUMBER = '" + phoneNumber + "' WHERE ID = " + contactId;
        stmt.executeUpdate(updateContactSQL);
        System.out.println("Contact with ID " + contactId + " updated.");

    }

    private static void viewAllContacts(Statement stmt) throws SQLException
    {
        String selectAllContactsSQL = "SELECT * FROM CONTACTS";
        try (ResultSet rs = stmt.executeQuery(selectAllContactsSQL)) {
            while (rs.next()) {
                int id = rs.getInt("ID");
                String firstName = rs.getString("FIRST_NAME");
                String lastName = rs.getString("LAST_NAME");
                String phoneNumber = rs.getString("PHONE_NUMBER");
                System.out.println("ID: " + id + ", First Name: " + firstName + ", Last Name: " + lastName + ", Phone Number: " + phoneNumber);
            }
        }
    }

    private static boolean contactExists(Statement stmt, int contactId) throws SQLException
    {
        String checkContactSQL = "SELECT COUNT(*) FROM CONTACTS WHERE ID = " + contactId;
        try (ResultSet rs = stmt.executeQuery(checkContactSQL)) {
            rs.next();
            return rs.getInt(1) > 0;
        }
    }


    // закриття ресурсів бази даних
    private static void closeResources(ResultSet rs, Statement stmt, Connection conn) {
        try {
            // перевірка та закриття resultset, statement і connection
            if (rs != null)
                rs.close();
            if (stmt != null)
                stmt.close();
            if (conn != null)
                conn.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}