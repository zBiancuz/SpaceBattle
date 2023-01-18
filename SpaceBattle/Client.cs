using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceBattle
{
    internal class Client
    {
        private TcpClient client;
        private Int32 port = 8080;
        private NetworkStream stream;
        ThreadRecive tr;

        public Client()
        {

        }
        public void Connect(String player)
        {
            try
            {
                client = new TcpClient("172.16.102.122", port);

                stream = client.GetStream();
                ThreadRecive thread = new ThreadRecive(client);

                Thread tr = new Thread(new ThreadStart(thread.run));
                tr.Start();


                Console.WriteLine("Connessione aperta");
                send("client: " + player + " pronto");

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }


            Console.Read();
        }

        public void send(string message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);


            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", message);
        }

        public void closeConnection()
        {
            // Close everything.
            stream.Close();
            client.Close();
        }
    }
}
