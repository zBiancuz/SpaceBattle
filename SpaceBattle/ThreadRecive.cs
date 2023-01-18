using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattle
{
    public class ThreadRecive
    {
        TcpClient client;
        public NetworkStream stream;
        Byte[] data;
        public ThreadRecive(TcpClient Client)
        {
            this.client = Client;   
        }

        public void run()
        {
            stream= client.GetStream();
            while (true)
            {
                Byte[] data = new Byte[256];

                String responseData = String.Empty;

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
            }
        }
    }
}
