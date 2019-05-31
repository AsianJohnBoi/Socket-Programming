using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace ServerSocketProgram
{
    class Program
    {
        const int PORT_NO = 2007;
        const string SERVER_IP = "127.0.0.1";

        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Parse(SERVER_IP), PORT_NO);
            serverSocket.Start();
            Console.WriteLine(">>> Server started on {0}:{1}", SERVER_IP, PORT_NO);

            //Incoming client connection
            TcpClient client = serverSocket.AcceptTcpClient();
            NetworkStream newStream = client.GetStream();
            if (client.Connected)
            {
                Console.WriteLine(">>> Client connected");
            }

            //get incoming data through network stream
            byte[] buffer = new byte[client.ReceiveBufferSize];

            //read incoming stream
            int bytesRead = newStream.Read(buffer, 0, client.ReceiveBufferSize);

            //Conver the data received into a string
            string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received : " + dataReceived);

            Console.WriteLine("Sending back : " + dataReceived);
            newStream.Write(buffer, 0, bytesRead);
            //client.Close();
            //serverSocket.Stop();
            Console.ReadLine();
        }
    }
}
