using System.Net;
using System.Net.Sockets;
using System.Text;
 
class UDPClient
{
    static void Main()
    {
        // UdpClient updClient = new UdpClient();
        // IPEndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 12345);
 
        // string message = "Hello world";
        // byte[] data = Encoding.UTF8.GetBytes(message);
 
        // updClient.Send(data, data.Length, serverEP);

        try
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, 5555));

            Console.WriteLine("Connected to server. Type 'exit' to quit.");

            while (true)
            {
                Console.WriteLine("Write message: ");
                string message = Console.ReadLine();

                if (message.ToLower() == "exit")
                {
                    break;
                }

                byte[] messageData = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(messageData);

                byte[] buffer = new byte[1024];
                int receiveBytes = clientSocket.Receive(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, receiveBytes);

                Console.WriteLine($"Server: {response}");
            }

            clientSocket.Close();
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}