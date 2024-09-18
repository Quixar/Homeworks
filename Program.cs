using System;

namespace student;

class AuthDataCollection
{
    private List<string> logins;
    private List<string> passwords;
    private List<string> keys;

    public AuthDataCollection()
    {
        logins = new List<string>();
        passwords = new List<string>();
        keys = new List<string>();
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

        string key = GenerateKey();
        keys.Add(key);
        string encryptedPassword = Encrypt(password, key);

        logins.Add(login);
        passwords.Add(encryptedPassword);
        System.Console.WriteLine("User was added");
    }

    public void DeleteUser(string login)
    {
        if(logins.Contains(login))
        {
            int index = logins.IndexOf(login);
            logins.RemoveAt(index);
            passwords.RemoveAt(index);
            keys.RemoveAt(index);
            System.Console.WriteLine("User was removed");
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
                Console.Write("Enter old password: ");
                string? oldPassword = Console.ReadLine();
                string decryptedOldPassword = Decrypt(passwords[index], keys[index]);

                if (oldPassword != decryptedOldPassword)
                {
                    throw new Exception("Old password is incorrect");
                }
                System.Console.Write("Set new password: ");
                string? newPassword = Console.ReadLine();
                passwords[index] = Encrypt(newPassword, keys[index]);
                System.Console.WriteLine("Users data was successfully updated");
            }
            else
            {
                throw new Exception("Invalid input");
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

    private string GetRepeatKey(string s, int n)
    {
        var r = s;
        while(r.Length < n)
        {
            r += r;
        }
        return r.Substring(0, n);
    }

    private string Cipher(string password, string secretKey)
    {
        var currentKey = GetRepeatKey(secretKey, password.Length);
        var result = string.Empty;
        for(int i = 0; i < password.Length; i++)
        {
            result += ((char)(password[i] ^ currentKey[i])).ToString();
        }
        return result;
    }

    private string Encrypt(string password, string key) => Cipher(password, key);
    private string Decrypt(string encryptedPassword, string key) => Cipher(encryptedPassword, key);

    private string GenerateKey()
    {
        const string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        int length = random.Next(8, 20);
        var key = new char[length];

        for(int i = 0; i < length; i++)
        {
            key[i] = symbols[random.Next(symbols.Length)];
        }
        return new string(key);
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