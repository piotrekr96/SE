using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace CommunicationServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tcpRun()
        {
            TcpListener tcpListnener = new TcpListener(IPAddress.Any, 5004);
            tcpListnener.Start();
            updateUI("Listening");

            while(true)
            {
                TcpClient client = tcpListnener.AcceptTcpClient();
                Thread tcpHandlerThread = new Thread(new ParameterizedThreadStart(tcpHandler));
                tcpHandlerThread.Start(client);
            }
        }

        private void tcpHandler(object client)
        {
            TcpClient mClient = (TcpClient)client;
            NetworkStream stream = mClient.GetStream();
            byte[] message = new byte[1024];

            stream.Read(message, 0, message.Length);
            updateUI("New Message = " + Encoding.ASCII.GetString(message));
            stream.Close();
            mClient.Close();
            
        }

        private void Start_Button(object sender, EventArgs e)
        {
            Thread tcpServerRunThread = new Thread(new ThreadStart(tcpRun));
            tcpServerRunThread.Start();
        }

        private void Box_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateUI(string s)
        {
            Func<int> del = delegate ()
            {
                Box.AppendText(s + System.Environment.NewLine);
                return 0;
            };

            Invoke(del);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Send_Click(object sender, EventArgs e)
        {
            Thread mThread = new Thread(new ThreadStart(SendAsServer));
            mThread.Start();
        }

        private void SendAsServer()
        {
            try
            {
                TcpClient client = new TcpClient();

                client.Connect(GetLocalIPAddress(), 5005);

                NetworkStream stream = client.GetStream();
                string s = "Hello from Server\n";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
                //updateUI("Message send");


                stream.Close();
                client.Close();
            }
            catch(Exception e)
            {

            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {

                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
