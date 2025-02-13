using System.Net;
using System.Net.Sockets;
using System.Text;

class UDPServer
{
    static void Main()
    {
        //  IPv4   0 - 256 = x.256.256.256
        // Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
 
        // IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 5000);
        // socket.Bind(endPoint);
 
        // socket.ReceiveTimeout = 1000;
 
        // Console.WriteLine($"Сервер прив'язаний до IP {endPoint.Address} Порт {endPoint.Port}");
        // Console.WriteLine($"Тайм-аут отримання {socket.ReceiveTimeout} мс");
 
        // socket.Close();

        //UdpClient udpServer = new UdpClient(12345);
        //IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        //Console.WriteLine("UDP сервер успішно запущений. Очікуємо повідомлення");

        //while (true)
        //{
        //    byte[] receivedData = udpServer.Receive(ref remoteEndPoint);
        //    string message = Encoding.UTF8.GetString(receivedData);

        //    Console.WriteLine($"Отримано від {remoteEndPoint}: {message}");
        //}

        // Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
 
        // socket.Bind(new IPEndPoint(IPAddress.Loopback, 5555));

        // socket.Listen(5);

        // System.Console.WriteLine("Server is waiting for conections");

        // Socket clientSocket = socket.Accept();

        // string message = "Hi, client";
        // byte[] data = Encoding.UTF8.GetBytes(message);

        // clientSocket.Send(data);
 
        // // Console.WriteLine($"Сервер прив'язаний до IP {endPoint.Address} Порт {endPoint.Port}");
        // Console.WriteLine($"Тайм-аут отримання {socket.ReceiveTimeout} мс");
 
        // clientSocket.Close();
        // socket.Close();

        try
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 5555));
            socket.Listen(5);

            Console.WriteLine("Server is waiting for connections...");

            Socket clientSocket = socket.Accept();
            Console.WriteLine("Client connected to server.");

            while (true)
            {
                byte[] buffer = new byte[1024];
                int receiveBytes = clientSocket.Receive(buffer);

                if (receiveBytes == 0)
                {
                    Console.WriteLine("Client disconnected.");
                    break;
                }

                string receiveMessage = Encoding.UTF8.GetString(buffer, 0, receiveBytes);
                Console.WriteLine($"Client: {receiveMessage}");

                string response = "Message was received";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                clientSocket.Send(responseData);
            }

            clientSocket.Close();
            socket.Close();
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Server error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}