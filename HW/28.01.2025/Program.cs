using System.IO.Compression;

internal class Program
{
    static Mutex mutex1 = new Mutex();
    static Mutex mutex2 = new Mutex();

    private static void Main(string[] args)
    {
        Thread thread = new Thread(GenerateNumbers);
        Thread thread2 = new Thread(FindPrime);
        Thread thread3= new Thread(FindPrime7);

        thread.Start();
        thread2.Start();
        thread3.Start();

        thread.Join();
        thread2.Join();
        thread3.Join();

    }

    static void GenerateNumbers()
    {
        mutex1.WaitOne();

        Random random = new Random();
        using (StreamWriter writer = new StreamWriter("numbers.txt"))
        {
            for (int i = 0; i < 50; i++)
            {
                writer.WriteLine(random.Next(1, 100));
            }
        }

        mutex1.ReleaseMutex();
    }

    static void FindPrime()
    {
        mutex1.WaitOne();
        mutex2.WaitOne();

        if(File.Exists("numbers.txt"))
        {
            var numbers = File.ReadAllLines("numbers.txt").Select(int.Parse).Where(IsPrime).ToList();

            File.WriteAllLines("prime.txt", numbers.Select(num => num.ToString()));
        }

        mutex1.ReleaseMutex();
        mutex2.ReleaseMutex();
    }

    static void FindPrime7()
    {
        mutex1.WaitOne();
        mutex2.WaitOne();
        if (File.Exists("prime.txt"))
        {
            var numbers = File.ReadAllLines("prime.txt").Select(int.Parse).Where(n => n % 10 == 7).ToList();

            File.WriteAllLines("prime7.txt", numbers.Select(n => n.ToString()));
        }
        mutex1.ReleaseMutex();
        mutex2.ReleaseMutex();
    }

    static bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }
}