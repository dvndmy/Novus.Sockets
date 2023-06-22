using System;
using System.Collections;
using System.Net.Sockets;

namespace ServerV2
{
    class ChatClient
    {
        // Hashtable to store all connected clients
        public static Hashtable AllClients = new Hashtable();

        private TcpClient _client;
        private string _clientIP;
        private string _ClientNick;
        private byte[] data;
        private bool ReceiveNick = true;

        public ChatClient(TcpClient client)
        {
            _client = client;
            _clientIP = client.Client.RemoteEndPoint.ToString();

            // Add the client to the hashtable of connected clients
            AllClients.Add(_clientIP, this);

            // Create a byte array to store received data
            data = new byte[_client.ReceiveBufferSize];

            // Begin asynchronous reading from the client's network stream
            _client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(_client.ReceiveBufferSize), ReceiveMessage, null);
        }

        public void SendMessage(string message)
        {
            try
            {
                System.Net.Sockets.NetworkStream ns;

                // Lock the client's network stream to ensure thread safety
                lock (_client.GetStream())
                {
                    ns = _client.GetStream();
                }

                // Convert the message string to bytes
                byte[] bytesToSend = System.Text.Encoding.ASCII.GetBytes(message);

                // Send the message to the client
                ns.Write(bytesToSend, 0, bytesToSend.Length);
                ns.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ReceiveMessage(IAsyncResult ar)
        {
            int bytesRead;
            try
            {
                // Lock the client's network stream to ensure thread safety
                lock (_client.GetStream())
                {
                    bytesRead = _client.GetStream().EndRead(ar);
                }

                if (bytesRead < 1)
                {
                    // If no bytes were read, remove the client from the hashtable and broadcast the leaving message
                    AllClients.Remove(_clientIP);
                    Broadcast(_ClientNick + " has left the chat.");
                    return;
                }
                else
                {
                    // Convert the received bytes to a string message
                    string messageReceived = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);

                    if (ReceiveNick)
                    {
                        // If it's the first message, set the client's nickname and broadcast the joining message
                        _ClientNick = messageReceived;
                        Broadcast(_ClientNick + " has joined the chat.");
                        ReceiveNick = false;
                    }
                    else
                    {
                        // Broadcast the message with the client's nickname
                        Broadcast(_ClientNick + ">" + messageReceived);
                    }
                }

                // Continue reading from the client's network stream
                lock (_client.GetStream())
                {
                    _client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(_client.ReceiveBufferSize), ReceiveMessage, null);
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs, remove the client from the hashtable and broadcast the leaving message
                AllClients.Remove(_clientIP);
                Broadcast(_ClientNick + " has left the chat. due to exception"+ex);
            }
        }

        public void Broadcast(string message)
        {
            Console.WriteLine(message);

            // Send the message to all connected clients
            foreach (DictionaryEntry c in AllClients)
            {
                ((ChatClient)(c.Value)).SendMessage(message + Environment.NewLine);
            }
        }
    }
}
