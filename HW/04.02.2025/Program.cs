internal class Program
    {
        private static readonly object lockObject = new object();
        private static Random random = new Random();
        private static int peopleAtStop = 0;
        private const int maxBusCapacity = 30;
        private const int totalBuses = 10;
        private static AutoResetEvent busArrived = new AutoResetEvent(false);

        static void Main()
        {
            try
            {
                Thread passengerThread = new Thread(GeneratePassengers);
                Thread busThread = new Thread(OperateBuses);

                passengerThread.Start();
                busThread.Start();

                passengerThread.Join();
                busThread.Join();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in main thread: {ex.Message}");
            }
        }

        private static void GeneratePassengers()
        {
            try
            {
                for (int i = 0; i < totalBuses; i++)
                {
                    lock (lockObject)
                    {
                        int newPassengers = random.Next(5, 20);
                        peopleAtStop += newPassengers;
                        Console.WriteLine($"{newPassengers} new passengers have arrived. Total at the stop: {peopleAtStop}");
                    }
                    Thread.Sleep(random.Next(1000, 3000));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in generating passangers: {ex.Message}");
            }
        }

        private static void OperateBuses()
        {
            try
            {
                for (int i = 1; i <= totalBuses; i++)
                {
                    Thread.Sleep(random.Next(2000, 5000));
                    lock (lockObject)
                    {
                        int boardingPassengers = Math.Min(peopleAtStop, maxBusCapacity);
                        peopleAtStop -= boardingPassengers;
                        Console.WriteLine($"Bus #175 has arrived. Boarded passengers: {boardingPassengers}. Left at the stop: {peopleAtStop}");
                    }
                    busArrived.Set();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bus error: {ex.Message}");
            }
        }
    }