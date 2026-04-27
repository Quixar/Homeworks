using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using UdpClient udpServer = new UdpClient(5002);
        IPEndPoint? clientEndPoint = new IPEndPoint(IPAddress.Any, 0);

        string[] moves = { "rock", "paper", "scissors" };
        int playerWins = 0, serverWins = 0;
        Random rnd = new Random();

        Console.WriteLine("Server started. Waiting for player moves...");

        for (int round = 1; round <= 5; round++)
        {
            UdpReceiveResult receiveResult = await udpServer.ReceiveAsync();
            clientEndPoint = receiveResult.RemoteEndPoint;
            string playerMove = Encoding.UTF8.GetString(receiveResult.Buffer).ToLower().Trim();

            string serverMove = moves[rnd.Next(moves.Length)];

            string result;
            if (playerMove == serverMove) result = "Draw";
            else if ((playerMove == "rock" && serverMove == "scissors") ||
                     (playerMove == "scissors" && serverMove == "paper") ||
                     (playerMove == "paper" && serverMove == "rock"))
            {
                result = "You won the round";
                playerWins++;
            }
            else
            {
                result = "Server won the round";
                serverWins++;
            }

            string status = $"{serverMove}|{result}|{playerWins}:{serverWins}";
            byte[] responseData = Encoding.UTF8.GetBytes(status);
            await udpServer.SendAsync(responseData, responseData.Length, clientEndPoint);

            Console.WriteLine($"Round {round}: Player ({playerMove}) - Server ({serverMove})");
        }

        string final = playerWins > serverWins ? "YOU WON THE GAME!" : 
                       serverWins > playerWins ? "SERVER WON THE GAME!" : "THE GAME IS A DRAW!";
        byte[] finalData = Encoding.UTF8.GetBytes(final);
        await udpServer.SendAsync(finalData, finalData.Length, clientEndPoint);

        Console.WriteLine("Game finished. Server shutting down.");
    }
}