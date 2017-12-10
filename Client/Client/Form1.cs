using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Connect_Click(object sender, EventArgs e)
        {
            Thread mThread = new Thread(new ThreadStart(ConnectAsClient));
            mThread.Start();
        }

        private void ConnectAsClient()
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("192.168.43.146"), 5004);

            NetworkStream stream =  client.GetStream();
            string s = "Hello from Client";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message, 0, message.Length);
            updateUI("Message send");


            stream.Close();
            client.Close();
        }

        private void updateUI(string s)
        {
            Func<int> del = delegate()
            {
                Box.AppendText(s + System.Environment.NewLine);
                return 0;
            };

            Invoke(del);
        }
    }
}
