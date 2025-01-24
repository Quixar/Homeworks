internal class Program
{
    private static bool running = true;
    static void Main()
    {
        int bottom = 2;
        int? top = null;
        System.Console.WriteLine("Enter bottom limit or press Enter (deffault 2): ");
        if (int.TryParse(Console.ReadLine(), out int bottomInput))
        {
            Validation(bottomInput);
            bottom = bottomInput;
        }
        System.Console.WriteLine("Enter top limit or press Enter (for inifinty generating): ");
        if (int.TryParse(Console.ReadLine(), out int topInput))
        {
            Validation(topInput);
            top = topInput;
        }
        Console.WriteLine("Press any button for program ending");
        Thread primeThread = new Thread(() => GeneratePrimes(bottom, top));
        primeThread.Start();
        Console.ReadKey();
        running = false;
        primeThread.Join();
    }
    private static void GeneratePrimes(int lowerBound, int? upperBound)
    {
        Random random = new Random();
        try
        {
            while (running)
            {
                int candidate = random.Next(lowerBound, upperBound ?? int.MaxValue);
                if (IsPrime(candidate))
                {
                    Console.WriteLine(candidate);
                    Thread.Sleep(100);
                }
            }
            Console.WriteLine("Generating end.");
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
        
    }
    private static bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
            if (number % i == 0) return false;
        
        return true;
    }
    private static void Validation(int number)
    {
        if (number < 0)
            throw new ArgumentException("Number must be more then 0.");
    }
}