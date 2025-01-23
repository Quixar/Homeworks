using System.Globalization;
using System.Numerics;

internal class Program
{
    // private static ManualResetEvent _pauseEvent = new ManualResetEvent(false);
    // private static void Main(string[] args)
    // {
    //     Thread squareThread = new Thread(CalculateSquare);
    //     squareThread.Start();
    //     System.Console.WriteLine("Press Enter");
    //     Console.ReadLine();

    //     _pauseEvent.Reset();
    //     System.Console.WriteLine("Press enter again");
    //     Console.ReadLine();

    //     _pauseEvent.Set();

    //     squareThread.Join();

    //     System.Console.WriteLine("Program has been ended");
    // }

    // static void CalculateSquare()
    // {
    //     for (int i = 0; i < 10; i++)
    //     {
    //         _pauseEvent.WaitOne();
    //         int square = i * i;
    //         System.Console.WriteLine($"Square of {i} = {square}");
    //         Thread.Sleep(500);
    //     }
    // }

    // static void Main()
    // {
    //     Thread highPriorityThread = new Thread(CalculateSum);
    //     Thread normalPriority = new Thread(PrintHelloWorld);
    //     Thread lowPriority = new Thread(CountEvenNumber);

    //     highPriorityThread.Priority = ThreadPriority.Highest;
    //     normalPriority.Priority = ThreadPriority.Normal;
    //     lowPriority.Priority = ThreadPriority.Lowest;

    //     highPriorityThread.Start();
    //     normalPriority.Start();
    //     lowPriority.Start();

    //     highPriorityThread.Join();
    //     lowPriority.Join();
    //     System.Console.WriteLine("Program has been ended");
    // }

    // static void CalculateSum()
    // {
    //     long sum = 0;
    //     for (int i = 0; i < 1000000; i++)
    //     {
    //         sum += i;
    //     }

    //     System.Console.WriteLine($"Sum of numbers from 1 to 1000000: {sum}");
    // }

    // static void CountEvenNumber()
    // {
    //     int count = 0;

    //     for (int i = 0; i < 1000000; i++)
    //     {
    //         if(i % 2 == 0)
    //         {
    //             count++;
    //         }
    //     }

    //     System.Console.WriteLine($"Count of even numbers from 1 to 1000000: {count}");
    // }

    // static void PrintHelloWorld()
    // {
    //     for (int i = 0; i < 5; i++)
    //     {
    //         System.Console.WriteLine("Hello World");
    //         Thread.Sleep(1000);
    //     }
    // }

    // static Random random = new Random();
    // private static List<int> numbers = new List<int>();

    // private static bool isGenerating = true;
    // private readonly static object Locker = new object();
    // static void Main()
    // {
    //     Thread thread = new Thread(GenerateRandomNumbers);
    //     Thread printThread = new Thread(Print);

    //     thread.Start();
    //     printThread.Start();

    //     thread.Join();
    //     printThread.Join();

    //     isGenerating = true;
    //     ThreadPool.QueueUserWorkItem(_ => GenerateRandomNumbers());
    //     ThreadPool.QueueUserWorkItem(_ => Print());

    //     Thread.Sleep(5000);
    //     isGenerating = false;
    //     System.Console.WriteLine("Program has been ended");
    // }

    // static void GenerateRandomNumbers()
    // {
    //     for (int i = 0; i < 20; i++)
    //     {
    //         int number = random.Next(1, 101);
    //         lock (Locker)
    //         {
    //             numbers.Add(number);
    //         }
    //     }
    // }

    // static void Print()
    // {
    //     // for (int i = 0; i < numbers.Count; i++)
    //     // {
    //     //     System.Console.WriteLine(numbers[i]);
    //     //     Thread.Sleep(1000);
    //     // }
    //     while(isGenerating || numbers.Count > 0)
    //     {
    //         lock (Locker)
    //         {
    //             if (numbers.Count > 0)
    //             {
    //                 int number = numbers[0];
    //                 numbers.RemoveAt(0);
    //                 System.Console.WriteLine(number);
    //             }
    //         }
    //         Thread.Sleep(1000);
    //     }
    // }

    
    static void Main()
    {
        for (int i = 0; i < 5; i++)
        {
            int taskNumber = i;
            ThreadPool.QueueUserWorkItem(TaskToDo, taskNumber);
        }

        Console.WriteLine("Press Enter to end the program");
        Console.ReadLine();
    }

    static void TaskToDo(object taskNumber)
    {
        System.Console.WriteLine($"Task {taskNumber} started at thread {Thread.CurrentThread.ManagedThreadId}");

        Thread.Sleep(2000);

        System.Console.WriteLine($"Task {taskNumber} ended at thread {Thread.CurrentThread.ManagedThreadId}");
    }
}