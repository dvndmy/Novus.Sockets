using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace WinClientV2
{
    public partial class Form1 : Form
    {
        const int portNo = 500;
        TcpClient client;
        byte[] data;

        public Form1()
        {
            InitializeComponent();
        }

        public void SendMessage(string message)
        {
            try
            {
                NetworkStream ns = client.GetStream();

                // Convert the message to bytes
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Send the message to the server
                ns.Write(data, 0, data.Length);
                ns.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                int bytesRead;
                bytesRead = client.GetStream().EndRead(ar);

                if (bytesRead < 1)
                {
                    return;
                }
                else
                {
                    // Convert the received bytes to a string message
                    string receivedMessage = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);

                    // Update the message history on the UI thread
                    object[] para = { receivedMessage };
                    this.Invoke(new delUpdateHistory(UpdateHistory), para);
                }

                // Continue reading from the server
                client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(client.ReceiveBufferSize), ReceiveMessage, null);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (btnSignIn.Text == "Sign In")
            {
                try
                {
                    client = new TcpClient();
                    client.Connect("127.0.0.1", portNo);

                    // Create a byte array to store received data
                    data = new byte[client.ReceiveBufferSize];

                    // Send the nickname to the server
                    SendMessage(txtNick.Text);

                    // Begin asynchronous reading from the server
                    client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(client.ReceiveBufferSize), ReceiveMessage, null);

                    btnSignIn.Text = "Sign Out";
                    btnSend.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                Disconnect();
                btnSignIn.Text = "Sign In";
                btnSend.Enabled = false;
            }
        }

        public void Disconnect()
        {
            try
            {
                // Close the network stream and disconnect from the server
                client.GetStream().Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public delegate void delUpdateHistory(string str);
        public void UpdateHistory(string str)
        {
            // Update the message history on the UI thread
            txtMessageHistory.AppendText(str);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Send the message to the server
            SendMessage(txtMessage.Text);
            txtMessage.Clear();
        }
    }
}
