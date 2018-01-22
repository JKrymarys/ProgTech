using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;



namespace BattleshipsOnline.Sources.TCPConnector
{
    class MyServer : WriterReader
    {
        public string hostName;
        public IPHostEntry myIPAddress;
        private Int32 port = 9999;
        private TcpListener server = null;
        private Thread listenThread = null;
        public  TcpClient tcpClient = null;
        public NetworkStream stream;
       
        public MyServer (){
            this.hostName = Dns.GetHostName();
            this.myIPAddress = Dns.GetHostEntry(hostName);
            startServer();
        }
        public void startServer()
        {
            server = new TcpListener(IPAddress.Any, this.port);
            server.Start();
            Console.WriteLine("server started");
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
            Console.WriteLine("thread started");
        }

        private void ListenForClients()
        {

            while (true)
            {
                //blocks until a client has connected to the server
                try
                {
                    this.tcpClient = this.server.AcceptTcpClient();
                    this.stream = tcpClient.GetStream();

                    IPEndPoint ip = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
                    Console.WriteLine("Client " + ip.ToString() + " has established connection");
                }
                catch (Exception e)
                {
                    break;

                }

            }

        }

        public String getMessage() {
            Byte[] data = new Byte[256];
            Int32 flag;

            flag = stream.Read(data, 0, data.Length);
            String message = System.Text.Encoding.ASCII.GetString(data);

            return message;
        }

        public void sendMessage(String message)
        {
            Byte[] data = new Byte[256];
            data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
    }
}
