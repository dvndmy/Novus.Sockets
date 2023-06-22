using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace ServerV3
{
    class Program
    {
        const int portNo = 500;

        static void Main(string[] args)
        {
            // Create an instance of the local IP address
            System.Net.IPAddress localAdd = System.Net.IPAddress.Parse("127.0.0.1");

            // Create a TCP listener on the specified local IP address and port number
            System.Net.Sockets.TcpListener listener = new System.Net.Sockets.TcpListener(localAdd, portNo);
            listener.Start();

            while (true)
            {
                // Accept a new TCP client connection
                ChatClient user = new ChatClient(listener.AcceptTcpClient());
            }
        }
    }
}
