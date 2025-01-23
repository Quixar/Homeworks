using System.Diagnostics;

internal class Program
{
    // static async Task Main()
    // {
    //     string[] urls = 
    //     {
    //         "https://www.google.com",
    //         "https://www.microsoft.com",
    //         "https://www.example.com"
    //     };

    //     Task<(string url, TimeSpan duraction)>[] tasks = new Task<(string url, TimeSpan duraction)>[urls.Length];

    //     for (int i = 0; i < urls.Length; i++)
    //     {
    //         string url = urls[i];
    //         tasks[i] = LoadPageAsync(url);
    //     }

    //     Stopwatch totalStopwatch = Stopwatch.StartNew();

    //     var results = await Task.WhenAll(tasks);

    //     totalStopwatch.Stop();

    //     System.Console.WriteLine($"Results of downloading pages");
    //     foreach (var result in results)
    //     {
    //         System.Console.WriteLine($"Page {result.url}, Duration: {result.duraction.Milliseconds} ms");
    //     }

    //     System.Console.WriteLine($"Total time of downloading {totalStopwatch.Elapsed.TotalMilliseconds} ms");
    // }

    // static async Task<(string url, TimeSpan duraction)> LoadPageAsync(string url)
    // {
    //     using HttpClient client = new HttpClient();
    //     Stopwatch stopwatch = Stopwatch.StartNew();

    //     await client.GetStringAsync(url);

    //     stopwatch.Stop();

    //     return (url, stopwatch.Elapsed);        
    // }

    // public async Task<string> DownloadFileAsync(string url)
    // {
    //     // HttpClient client = new HttpClient();

    //     // string result = await client.GetStringAsync(url);
    //     // return result;

    //     // await Task.Delay(2000);
    //     // System.Console.WriteLine("Procces has been ended");
    // }

    // public delegate Task AsyncOperation(string url);

    // public async Task DownloadFileAsync(string url)
    // {
    //     HttpClient client = new HttpClient();
    //     string result = await client.GetStringAsync(url);
    //     System.Console.WriteLine($"File has been downloaded from {url}");
    // }

    // public async Task ExecuteOperationAsync()
    // {
    //     AsyncOperation operation = new AsyncOperation(DownloadFileAsync);
    //     await operation("https://example.com");
    // }

    // static async Task Main(string[] args)
    // {
        
    // }

    // public delegate Task AsyncCallBack(string result);

    // public async Task ProcessDataAsync(AsyncCallBack callBack)
    // {
    //     await Task.Delay(1000);
    //     string result = "Data is updating";
    //     await callBack(result);
    // }

    // public async Task CallBackMethod(string result)
    // {
    //     System.Console.WriteLine($"Received {result}");
    //     await Task.CompletedTask;
    // }

    // public async Task ExecuteAsync()
    // {
    //     AsyncCallBack callBack = new AsyncCallBack(CallBackMethod);
    //     await ProcessDataAsync(callBack);
    // }

    // private static Timer? _timer;
    // static async Task Main()
    // {
    //     TimerCallback  callback = new TimerCallback(async _ => await ExecuteTaskAsync());

    //     _timer = new Timer(callback, null, 2000, Timeout.Infinite);

    //     Thread.Sleep(5000);
    //     _timer.Change(2000, 1000);
    //     Console.ReadLine();
    // }

    // public static async Task ExecuteTaskAsync()
    // {   
    //     await Task.Delay(1000);
    //     System.Console.WriteLine($"Timer is calling {DateTime.Now}");
    // }

    static void Main()
    {
        System.Console.WriteLine("Main thread started");

        ThreadPool.GetMinThreads(out int minWorker, out int minIO);
        ThreadPool.GetMaxThreads(out int maxWorker, out int maxIO);

        System.Console.WriteLine($"Min threads {minWorker} {minIO}");
        System.Console.WriteLine($"Max threads {maxWorker} {minIO}");

        ThreadPool.SetMinThreads(4, 4);
        ThreadPool.SetMaxThreads(16, 16);

        for (int i = 0; i <= 10; i++)
        {
            int taskNumber = 1;
            ThreadPool.QueueUserWorkItem(state => 
            {
                System.Console.WriteLine($"Task {taskNumber} is execute in thread {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(500);
            });
        }

        Thread.Sleep(2000);

        ThreadPool.QueueUserWorkItem(ExecuteTask, "task 1");
        ThreadPool.QueueUserWorkItem(ExecuteTask, "task 2");

        Thread.Sleep(1000);
    }

    static void ExecuteTask(object? state)
    {
        System.Console.WriteLine($"{state}: is executing in thread {Thread.CurrentThread.ManagedThreadId}");
        System.Console.WriteLine($"{state} ended");
    }
}