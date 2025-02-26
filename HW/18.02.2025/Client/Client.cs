using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Client
{
    static void Main(string[] args)
    {
        try
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, 5555));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Connected to the server.");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Enter command (usd euro / euro usd / exit): ");
                Console.ResetColor();
                string message = Console.ReadLine();

                byte[] data = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(data);

                if (message.ToLower() == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Disconnected from server.");
                    Console.ResetColor();
                    break;
                }

                byte[] buffer = new byte[1024];
                int receivedBytes = clientSocket.Receive(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Server: {response}");
                Console.ResetColor();
            }

            clientSocket.Close();
        }
        catch (SocketException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Socket error: {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Unexpected error: {ex.Message}");
            Console.ResetColor();
        }
    }
}
