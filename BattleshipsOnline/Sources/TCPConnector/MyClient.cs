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
    class MyClient : WriterReader
    {
        private String host;
        private Int32 port = 9999;
        private TcpClient client;
        private NetworkStream stream;
        public MyClient(String host)
        {
            this.client = new TcpClient(host, port);
            this.stream = client.GetStream();
        }
        public void sendMessage(String message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
        public String getMessage()
        {
            Byte[] data = new Byte[7];
            Int32 flag = stream.Read(data, 0, data.Length);
            return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}
