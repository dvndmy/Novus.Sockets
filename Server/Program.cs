using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        const int portNo = 500;

        static void Main(string[] args)
        {
            // Create an IP address object for the local address
            System.Net.IPAddress localAdd = System.Net.IPAddress.Parse("127.0.0.1");

            // Create a TCP listener bound to the local address and port number
            TcpListener listener = new TcpListener(localAdd, portNo);

            // Start listening for incoming connection requests
            listener.Start();

            // Accept a pending connection request and return a TcpClient to handle communication with the client
            TcpClient tcpClient = listener.AcceptTcpClient();

            // Get the network stream for sending and receiving data
            NetworkStream ns = tcpClient.GetStream();

            // Create a byte array to store received data
            byte[] data = new byte[tcpClient.ReceiveBufferSize];

            // Read data from the network stream into the byte array and get the number of bytes read
            int numBytesRead = ns.Read(data, 0, System.Convert.ToInt32(tcpClient.ReceiveBufferSize));

            // Convert the received data to a string and display it
            Console.WriteLine("Received: " + Encoding.ASCII.GetString(data, 0, numBytesRead));

            // Wait for the user to press enter before exiting
            Console.ReadLine();

            // Clean up resources
            ns.Close();
            tcpClient.Close();
            listener.Stop();
        }
    }
}
