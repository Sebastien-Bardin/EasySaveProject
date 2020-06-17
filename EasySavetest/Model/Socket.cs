using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

using System.Threading;

namespace EasySavetest.Model
{

    class Socket
    {

        static readonly object _lock = new object();
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();

        /// <summary>
        /// Socket class to set a communication
        /// </summary>
        public Socket()
        {
            int count = 1;

            //Initilize the communication
            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 5000);
            ServerSocket.Start();

            while (true)
            {

                //Accept a remote connexion
                TcpClient client = ServerSocket.AcceptTcpClient();

                //Add th connexion to a list of client
                lock (_lock) list_clients.Add(count, client);

                //Listen the client
                Thread t = new Thread(handle_clients);


                t.Start(count);
                count++;
            }
        }

        /// <summary>
        /// Listen the client
        /// </summary>
        /// <param name="o"></param>
        public static void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;

            lock (_lock) client = list_clients[id];

            while (true)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int byte_count = stream.Read(buffer, 0, buffer.Length);

                    if (byte_count == 0)
                    {
                        break;
                    }
                }
                catch
                {
                    break;
                }
            }

            lock (_lock) list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        /// <summary>
        /// Send data to the client
        /// </summary>
        /// <param name="data"></param>
        public static void broadcast(string data)
        {
            //Prepare the message
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            lock (_lock)
            {
                //Send the message to the client
                foreach (TcpClient c in list_clients.Values)
                {

                    NetworkStream stream = c.GetStream();

                    stream.Write(buffer, 0, buffer.Length);

                }
            }
        }


    }
}
