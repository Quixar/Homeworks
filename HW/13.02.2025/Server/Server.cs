using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Server
{
    private static string serverPath = Path.Combine(Environment.CurrentDirectory, "loggs.txt");
    private static double exchangeRateEURtoUSD = 1.18;
    private static double exchangeRateUSDtoEUR = 1 / exchangeRateEURtoUSD;

    static void Main(string[] args)
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket clientSocket = null;

        try
        {
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 5555));
            socket.Listen(5);

            LogMessage("Server is waiting for connections...", ConsoleColor.Green);

            clientSocket = socket.Accept();
            IPEndPoint clientEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
            string clientIp = clientEndPoint.Address.ToString();

            LogMessage($"Client connected to server. IP: {clientIp}, Time: {DateTime.Now}", ConsoleColor.Green);

            while (true)
            {
                byte[] buffer = new byte[1024];
                int receivedBytes = clientSocket.Receive(buffer);

                if (receivedBytes == 0)
                {
                    LogMessage($"Client {clientIp} disconnected.", ConsoleColor.Yellow);
                    break;
                }

                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                LogMessage($"Client {clientIp}: {receivedMessage}", ConsoleColor.Cyan);

                string response;
                if (receivedMessage.ToLower() == "exit")
                {
                    LogMessage($"Client {clientIp} disconnected.", ConsoleColor.Yellow);
                    break;
                }
                else if (receivedMessage.ToLower() == "usd euro")
                {
                    response = $"USD to EURO: {exchangeRateUSDtoEUR}";
                }
                else if (receivedMessage.ToLower() == "euro usd")
                {
                    response = $"EURO to USD: {exchangeRateEURtoUSD}";
                }
                else
                {
                    response = "Data was entered incorrectly";
                    LogMessage(response, ConsoleColor.Red);
                }

                byte[] responseData = Encoding.UTF8.GetBytes(response);
                clientSocket.Send(responseData);
                LogMessage($"Server sent message to {clientIp} at {DateTime.Now}", ConsoleColor.Green);
            }
        }
        catch (SocketException ex)
        {
            LogError("Server error", ex.Message);
        }
        catch (Exception ex)
        {
            LogError("Unexpected error", ex.Message);
        }
        finally
        {
            clientSocket?.Close();
            socket.Close();
            LogMessage("Server has finished working", ConsoleColor.Green);
        }
    }

    private static void LogMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
        File.AppendAllText(serverPath, message + Environment.NewLine);
    }

    private static void LogError(string errorType, string errorMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{errorType}: ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(errorMessage);
        Console.ResetColor();
        File.AppendAllText(serverPath, $"{errorType}: {errorMessage}{Environment.NewLine}");
    }
}
