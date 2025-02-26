using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

internal class Server
{
    private static string serverPath = Path.Combine(Environment.CurrentDirectory, "loggs.txt");
    private static double exchangeRateEURtoUSD = 1.18;
    private static double exchangeRateUSDtoEUR = 1 / exchangeRateEURtoUSD;
    private static int maxConnections = 5;
    private static int currentConnections = 0;
    private static readonly Dictionary<string, (int requestCount, DateTime lastRequestTime)> clientRequests = new();
    private static int maxRequests = 5;
    private static TimeSpan blockTime = TimeSpan.FromMinutes(1);

    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5555);
        listener.Start();
        LogMessage("Server started. Waiting for connections...", ConsoleColor.Green);

        while (true)
        {
            if (currentConnections >= maxConnections)
            {
                LogMessage("Server is at full capacity. Rejecting new connections.", ConsoleColor.Red);
                Thread.Sleep(5000);
                continue;
            }

            TcpClient client = listener.AcceptTcpClient();
            Interlocked.Increment(ref currentConnections);
            ThreadPool.QueueUserWorkItem(HandleClient, client);
        }
    }

    static void HandleClient(object obj)
    {
        using TcpClient client = (TcpClient)obj;
        using NetworkStream stream = client.GetStream();
        IPEndPoint endPoint = (IPEndPoint)client.Client.RemoteEndPoint;
        string clientAddress = endPoint.Address.ToString();
        LogMessage($"Client connected: {clientAddress}:{endPoint.Port}", ConsoleColor.Green);

        try
        {
            if (clientRequests.ContainsKey(clientAddress) &&
                clientRequests[clientAddress].requestCount >= maxRequests &&
                DateTime.Now - clientRequests[clientAddress].lastRequestTime < blockTime)
            {
                LogMessage($"Client {clientAddress} exceeded request limit. Blocking.", ConsoleColor.Red);
                byte[] blockMessage = Encoding.UTF8.GetBytes("Request limit exceeded. Try again later.");
                stream.Write(blockMessage, 0, blockMessage.Length);
                return;
            }

            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                LogMessage($"Received request from {clientAddress}: {request}", ConsoleColor.Cyan);

                if (!clientRequests.ContainsKey(clientAddress))
                {
                    clientRequests[clientAddress] = (0, DateTime.Now);
                }

                var clientData = clientRequests[clientAddress];
                clientRequests[clientAddress] = (clientData.requestCount + 1, DateTime.Now);

                string response = request.ToLower() switch
                {
                    "usd euro" => $"USD to EURO: {exchangeRateUSDtoEUR}",
                    "euro usd" => $"EURO to USD: {exchangeRateEURtoUSD}",
                    "exit" => "Goodbye!",
                    _ => "Invalid request"
                };

                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);
                LogMessage($"Sent response to {clientAddress}: {response}", ConsoleColor.White);

                if (request.ToLower() == "exit")
                    break;
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Error: {ex.Message}", ConsoleColor.Red);
        }

        LogMessage($"Client {clientAddress} disconnected", ConsoleColor.Yellow);
        Interlocked.Decrement(ref currentConnections);
    }

    private static void LogMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
        File.AppendAllText(serverPath, message + Environment.NewLine);
    }
}
