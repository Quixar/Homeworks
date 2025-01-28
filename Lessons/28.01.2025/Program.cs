internal class Program
{
    // static List<int> sharedList = new List<int>();

    // static void AddToList()
    // {
    //     for (int i = 0; i < 5; i++)
    //     {
    //         sharedList.Add(i);
    //         System.Console.WriteLine($"Added {i} thread {Thread.CurrentThread.ManagedThreadId}");
    //     }
    // }
    // private static void Main(string[] args)
    // {
    //     Thread t1 = new Thread(AddToList);
    //     Thread t2 = new Thread(AddToList);

    //     t1.Start();
    //     t2.Start();

    //     t1.Join();
    //     t2.Join();

    //     System.Console.WriteLine("Final result");
    //     sharedList.ForEach(i => System.Console.WriteLine(i));
    // }

    // static object resource1 = new object();
    // static object resource2 = new object();

    // static void Thread1()
    // {
    //     lock (resource1)
    //     {
    //         System.Console.WriteLine("Thread 1: resource 1 blocked");
    //         Thread.Sleep(100);
    //         lock (resource2)
    //         {
    //             System.Console.WriteLine("Thread 1: resource 2 blocked");
    //         }
    //     }
    // }

    // static void Thread2()
    // {
    //     lock (resource2)
    //     {
    //         System.Console.WriteLine("Thread 1: resource 2 blocked");
    //         Thread.Sleep(100);
    //         lock (resource1)
    //         {
    //             System.Console.WriteLine("Thread 1: resource 1 blocked");
    //         }
    //     }
    // }
    // static void Main()
    // {
    //     Thread t1 = new Thread(Thread1);
    //     Thread t2 = new Thread(Thread2);

    //     t1.Start();
    //     t2.Start();

    //     t1.Join();
    //     t2.Join();

    // }

    // private static List<int> sharedList = new List<int>();
    // static object lockObject = new object();

    // static void WriteToList()
    // {
    //     lock (lockObject)
    //     {
    //         for (int i = 0; i < 5; i++)
    //         {
    //             sharedList.Add(i);
    //             System.Console.WriteLine($"Aded {i} {Thread.CurrentThread.ManagedThreadId}");
    //             Thread.Sleep(50);
    //         }
    //     }
    // }

    // static void ReadFromList()
    // {
    //     lock(lockObject)
    //     {
    //         foreach (var item in sharedList)
    //         {
    //             System.Console.WriteLine($"Recived {item} {Thread.CurrentThread.ManagedThreadId}");   
    //             Thread.Sleep(50);         
    //         }
    //     }
    // }

    // static void Main()
    // {
    //     Thread writer = new Thread(WriteToList);
    //     Thread reader = new Thread(ReadFromList);

    //     writer.Start();
    //     reader.Start();

    //     writer.Join();
    //     reader.Join();
    // }


    // private static Mutex mutex = new Mutex();

    // static void AccessResource(string threadName)
    // {
    //     System.Console.WriteLine($"{threadName} is waiting for resource");
    //     mutex.WaitOne();
    //     System.Console.WriteLine($"{threadName} received access to resource");
    //     Thread.Sleep(2000);
    //     System.Console.WriteLine($"{threadName} is freeing mutex");
    //     mutex.ReleaseMutex();
    // }
    // static void Main()
    // {
    //     Thread writer = new Thread(() => AccessResource("Thread 1"));
    //     Thread reader = new Thread(() => AccessResource("Thread 2"));

    //     writer.Start();
    //     reader.Start();

    //     writer.Join();
    //     reader.Join();
    // }


    // static void Main()
    // {
    //     string mutexName = "Global\\Mutex";
    //     using (Mutex mutex = new Mutex(false, mutexName))
    //     {
    //         System.Console.WriteLine("Waiting for resource access");
    //         if (mutex.WaitOne(TimeSpan.FromSeconds(10)))
    //         {
    //             try 
    //             {
    //                 System.Console.WriteLine("Resource not free, press Enter");
    //                 Console.ReadLine();
    //             }
    //             finally
    //             {
    //                 mutex.ReleaseMutex();
    //                 System.Console.WriteLine("Resource was freeing");
    //             }
    //         }
    //         else
    //         {
    //             System.Console.WriteLine("Error");
    //         }
    //     }
    // }

    // static List<int> sharedList = new List<int>();
    // static Mutex mutex = new Mutex();

    // static void Adding()
    // {
    //     for (int i = 0; i < 10; i++)
    //     {
    //         mutex.WaitOne();
    //         try
    //         {
    //             sharedList.Add(i);
    //             System.Console.WriteLine($" Thread {Thread.CurrentThread.ManagedThreadId} added {i}");
    //         }
    //         finally
    //         {
    //             mutex.ReleaseMutex();
    //         }
    //         Thread.Sleep(100);
    //     }
    // }

    // static void Main()
    // {
    //     Thread t1 = new Thread(Adding);
    //     Thread t2 = new Thread(Adding);

    //     t1.Start();
    //     t2.Start();

    //     t1.Join();
    //     t2.Join();

    //     foreach (var item in sharedList)
    //     {
    //         System.Console.WriteLine(item);
    //     }
    // }

    // static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);

    // static async Task ProcessDataAsync(int taskId)
    // {
    //     System.Console.WriteLine($"Task {taskId} is waiting for access");
    //     await semaphoreSlim.WaitAsync();

    //     try
    //     {
    //         System.Console.WriteLine($"Task {taskId} is working");
    //         await Task.Delay(2000);
    //     }
    //     finally
    //     {
    //         System.Console.WriteLine($"Task {taskId} ended");
    //         semaphoreSlim.Release();
    //     }
    // }

    // static async Task Main()
    // {
    //     var tasks = new Task[10];

    //     for (int i = 0; i < tasks.Length; i++)
    //     {
    //         tasks[i] = ProcessDataAsync(i);
    //     }
    //     await Task.WaitAll(tasks);
    // }
}