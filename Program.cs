using System;

namespace student;

delegate void Choice(int index);

class Program
{
    static void NewGame(int index)
    {
        System.Console.WriteLine("New game was started");
    }

    static void LoadGame(int index)
    {
        System.Console.WriteLine("Game was loaded");
    }

    static void Rules(int index)
    {
        System.Console.WriteLine("Some rules");
    }

    static void Author(int index)
    {
        System.Console.WriteLine("Author good boy");
    }

    static void Exit(int index)
    {
        System.Console.WriteLine("bye bye ");
    }

    static void Main(string[] args)
    {
        Console.Clear();
        Choice c = null;
        System.Console.WriteLine("1 - New game\n2 - Load game\n3 - Rules\n4 - About author\n0 - Exit");
        System.Console.Write("Make your choice: ");
        int choice = Convert.ToInt32(Console.ReadLine());
        if(choice == 1)
        {
            c += NewGame;
        }
        else if(choice == 2)
        {
            c += LoadGame;
        }
        else if (choice == 3)
        {
            c += Rules;
        }
        else if (choice == 4)
        {
            c += Author;
        }
        else
        {
            c += Exit;
        }
        c(choice);
    }
}