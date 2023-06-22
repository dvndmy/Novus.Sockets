using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        const int portNo = 500;

        static void Main(string[] args)
        {
            TcpClient tcpclient = new TcpClient();

            // Connect to the server at the specified IP address and port number
            tcpclient.Connect("127.0.0.1", portNo);

            // Get the network stream for sending and receiving data
            NetworkStream ns = tcpclient.GetStream();

            // Convert the string message to a byte array
            byte[] data = Encoding.ASCII.GetBytes("Hello World");

            // Send the data to the server
            ns.Write(data, 0, data.Length);
            Console.WriteLine("Sent : Hello World");
            // Wait for the user to press enter before exiting
            Console.ReadLine();

            // Clean up resources
            ns.Close();
            tcpclient.Close();
        }
    }
}
