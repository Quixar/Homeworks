using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using UdpClient udpClient = new UdpClient();
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5002);

        try
        {
            for (int i = 1; i <= 5; i++)
            {
                string move = "";
                while (move != "rock" && move != "paper" && move != "scissors")
                {
                    Console.Write($"\nRound {i}. Enter your move (rock, paper, scissors): ");
                    move = Console.ReadLine()?.ToLower().Trim() ?? "";
                }

                byte[] dataToSend = Encoding.UTF8.GetBytes(move);
                await udpClient.SendAsync(dataToSend, dataToSend.Length, serverEndPoint);

                UdpReceiveResult result = await udpClient.ReceiveAsync();
                string response = Encoding.UTF8.GetString(result.Buffer);

                var parts = response.Split('|');
                if (parts.Length == 3)
                {
                    Console.WriteLine($"-> Server chose: {parts[0]}");
                    Console.WriteLine($"-> Result: {parts[1]}");
                    Console.WriteLine($"-> Current Score: {parts[2]}");
                }
            }

            UdpReceiveResult finalResult = await udpClient.ReceiveAsync();
            Console.WriteLine(Encoding.UTF8.GetString(finalResult.Buffer));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }

        Console.WriteLine("\nPress Enter to exit...");
        Console.ReadLine();
    }
}