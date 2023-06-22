using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace ServerV2
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

            // Continuously accept and handle incoming connections
            while (true)
            {
                // Accept a pending connection request and return a TcpClient to handle communication with the client
                ChatClient user = new ChatClient(listener.AcceptTcpClient());

            }
        }
    }

 
}
