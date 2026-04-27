using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Commands: 'rock', 'paper', 'scissors', 'draw' (offer a draw), 'surrender' (admit defeat)");

        using UdpClient udpClient = new UdpClient();
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5002);

        try
        {
            bool keepPlaying = true;
            int round = 1;

            while (keepPlaying && round <= 5)
            {
                Console.Write($"\nRound {round}. Enter your move or command: ");
                string input = Console.ReadLine()?.ToLower().Trim() ?? "";

                if (string.IsNullOrEmpty(input)) continue;

                byte[] dataToSend = Encoding.UTF8.GetBytes(input);
                await udpClient.SendAsync(dataToSend, dataToSend.Length, serverEndPoint);

                UdpReceiveResult result = await udpClient.ReceiveAsync();
                string response = Encoding.UTF8.GetString(result.Buffer);
                string[] parts = response.Split('|');

                if (parts[0] == "EXIT")
                {
                    Console.WriteLine($"\n>>> {parts[1]}");
                    keepPlaying = false;
                }
                else if (parts[0] == "CONT" || parts[0] == "LAST")
                {
                    Console.WriteLine($"-> Server chose: {parts[1]}");
                    Console.WriteLine($"-> Round Result: {parts[2]}");
                    Console.WriteLine($"-> Current Score: {parts[3]}");

                    if (parts[0] == "LAST")
                    {
                        Console.WriteLine("\n==============================");
                        Console.WriteLine(parts[4]);
                        Console.WriteLine("==============================");
                        keepPlaying = false;
                    }
                    round++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\nDisconnected. Press Enter to exit...");
        Console.ReadLine();
    }
}