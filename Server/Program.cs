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
        bool gameFinished = false;

        Console.WriteLine("Server started. Waiting for player actions...");

        for (int round = 1; round <= 5 && !gameFinished; round++)
        {
            UdpReceiveResult receiveResult = await udpServer.ReceiveAsync();
            clientEndPoint = receiveResult.RemoteEndPoint;
            string playerInput = Encoding.UTF8.GetString(receiveResult.Buffer).ToLower().Trim();

            string responseMessage;
            byte[] responseData;

            if (playerInput == "surrender")
            {
                responseMessage = "EXIT|You surrendered. Server wins the game!";
                responseData = Encoding.UTF8.GetBytes(responseMessage);
                await udpServer.SendAsync(responseData, responseData.Length, clientEndPoint);
                Console.WriteLine("Player surrendered.");
                gameFinished = true;
                break;
            }

            if (playerInput == "draw")
            {
                responseMessage = "EXIT|Draw offered and accepted. The game ends here.";
                responseData = Encoding.UTF8.GetBytes(responseMessage);
                await udpServer.SendAsync(responseData, responseData.Length, clientEndPoint);
                Console.WriteLine("Game ended with a draw offer.");
                gameFinished = true;
                break;
            }

            string serverMove = moves[rnd.Next(moves.Length)];
            string result;

            if (playerInput == serverMove) result = "Draw";
            else if ((playerInput == "rock" && serverMove == "scissors") ||
                     (playerInput == "scissors" && serverMove == "paper") ||
                     (playerInput == "paper" && serverMove == "rock"))
            {
                result = "You won the round";
                playerWins++;
            }
            else
            {
                result = "Server won the round";
                serverWins++;
            }

            if (round == 5)
            {
                string finalResult = playerWins > serverWins ? "YOU WON THE GAME!" : 
                                   serverWins > playerWins ? "SERVER WON THE GAME!" : "THE GAME IS A DRAW!";
                responseMessage = $"LAST|{serverMove}|{result}|{playerWins}:{serverWins}|{finalResult}";
            }
            else
            {
                responseMessage = $"CONT|{serverMove}|{result}|{playerWins}:{serverWins}";
            }

            responseData = Encoding.UTF8.GetBytes(responseMessage);
            await udpServer.SendAsync(responseData, responseData.Length, clientEndPoint);
            Console.WriteLine($"Round {round}: Player ({playerInput}) - Server ({serverMove})");
        }

        Console.WriteLine("Server session finished.");
    }
}