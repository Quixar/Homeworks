using System;

namespace student;

class AuthDataCollection
{
    private List<string> logins;
    private List<string> passwords;

    public AuthDataCollection()
    {
        logins = new List<string>();
        passwords = new List<string>();
    }

    public int Count
    {
        get { return logins.Count;}
    }

    public string this[int index]
    {
        get
        {
            if(index < 0 || index >= logins.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return logins[index];
        }
    }

    public void AddUser(string login, string password)
    {
        if(logins.Contains(login))
        {
            throw new ArgumentException("This login already exist");
        }
        logins.Add(login);
        passwords.Add(password);
        System.Console.WriteLine("User: " + login + " added");
    }

    public void DeleteUser(string login)
    {
        if(logins.Contains(login))
        {
            int index = logins.IndexOf(login);
            logins.RemoveAt(index);
            passwords.RemoveAt(index);
            System.Console.WriteLine("User: " + login + " removed");
        }
        else{
            throw new ArgumentException("User not found");
        }
    }

    public void EditUser()
    {
        System.Console.WriteLine("Which user data you want to edit?");
        string? oldLogin = Console.ReadLine();
        if(logins.Contains(oldLogin))
        {
            int index = logins.IndexOf(oldLogin);
            System.Console.WriteLine("What do you want to change:\n1 - Login\n2 - Password");
            string? choice = Console.ReadLine();
            if(choice == "1")
            {
                System.Console.Write("Set new login: ");
                string? newLogin = Console.ReadLine();
                if(logins.Contains(newLogin))
                {
                    throw new Exception("User with this login already exists");
                }
                logins[index] = newLogin;
                System.Console.WriteLine("Users data was successfully updated");
            }
            else if (choice == "2")
            {
                System.Console.Write("Set new password: ");
                string? newPassword = Console.ReadLine();
                passwords[index] = newPassword;
                System.Console.WriteLine("Users data was successfully updated");
            }
        }
        else
        {
            throw new ArgumentException("This login doesnt exist");
        }
    }

    public void ShowUsers()
    {
        foreach(string login in logins)
        {
            System.Console.WriteLine(login);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        AuthDataCollection data = new AuthDataCollection();
        data.AddUser("test", "1234");
        data.AddUser("test2", "1234");
        data.AddUser("test3", "1234");
        data.AddUser("test4", "1234");
        data.AddUser("test1", "1234");
        data.DeleteUser("test1");
        data.EditUser();
        data.ShowUsers();
    }
}   