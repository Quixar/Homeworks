using System;
using System.Collections.Generic;
using System.Linq;

namespace student;

class Program
{
    static void AddBook(Dictionary<string, List<string>> library)
    {
        System.Console.Write("Write author name: ");
        string? author = Console.ReadLine();
        System.Console.Write("Write book name: ");
        string? book = Console.ReadLine();
        if(!library.ContainsKey(author))
        {
            library[author] = new List<string> {book};
        }
        else
        {
            library[author].Add(book);
        }
    }

    static void RemoveBook(Dictionary<string, List<string>> library)
    {
        System.Console.Write("Write author name: ");
        string? author = Console.ReadLine();
        string? book;
        if(!library.ContainsKey(author))
        {
            System.Console.WriteLine("We dont have any books by this author");
        }
        else
        {
            System.Console.Write("Write which book you want to delete: ");
            book = Console.ReadLine();

            if(library[author].Contains(book))
            {
                library[author].Remove(book);
            }
            else
            {
                System.Console.WriteLine("we dont have this book");
            }
        }
    }

    static void SearchBookAtAllLibrary(Dictionary<string, List<string>> library)
    {
        System.Console.WriteLine("Which book you want to find: ");
        string? book = Console.ReadLine();
        foreach(var key in library.Keys)
        {
            if(library[key].Contains(book))
            {
                System.Console.WriteLine($"Author of the book you're looking for: {key}");
                return;
            }
        }
        System.Console.WriteLine("We dont have this book");
    }

    static void ShowAllBookOfAuthor(Dictionary<string, List<string>> library)
    {
        System.Console.WriteLine("Write author whose books you want to see: ");
        string? author = Console.ReadLine();
        if(library.ContainsKey(author))
        {
            System.Console.WriteLine($"Books by {author}: {string.Join(", ", library[author])}");
        }
        else
        {
            System.Console.WriteLine("We dont have this author");
        }
    }

    static void ShowAllLibrary(Dictionary<string, List<string>> library)
    {
        foreach( var elem in library)
        {
            Console.WriteLine($"Author: {elem.Key}, Books: {string.Join(", ", elem.Value)}");
        }
    }

    public delegate void Choice(Dictionary<string, List<string>> library);

    static void Main(string[] args)
    {
        Console.Clear();
        Dictionary<string, List<string>> library = new Dictionary<string, List<string>>();

        Choice choice = null;

        while(true)
        {
            //Console.Clear();
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Remove a book");
            Console.WriteLine("3. Search for a book");
            Console.WriteLine("4. Show all books by an author");
            Console.WriteLine("5. Show the entire library");
            Console.WriteLine("0. Exit");
            System.Console.Write("Enter your choice: ");

            string? userChoice = Console.ReadLine();

            if(userChoice == "1")
            {
                choice = AddBook;
            }
            else if (userChoice == "2")
            {
                choice = RemoveBook;
            }
            else if(userChoice == "3")
            {
                choice = SearchBookAtAllLibrary;
            }
            else if(userChoice == "4")
            {
                choice = ShowAllBookOfAuthor;
            }
            else if (userChoice == "6")
            {
                choice = ShowAllLibrary;
            }
            else if (userChoice == "0")
            {
                break;
            }
            else
            {
                System.Console.WriteLine("Invalid choice, try again");
            }
            choice?.Invoke(library);
        }
    }
}