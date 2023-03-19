using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
/// Midterm For Networking
/// Students: Ricardo Prato 100787893, Ryan Dinh 100804962
/// This is the server that relays information to a unity project  

namespace MidtermServer
{
    public class Program
    {
        private static Socket serverSocket;
        private static Socket client;

        private static byte[] buffer = new byte[512];
        private static float[] pos = { 0f, 0f, 0f };


        private void ServerStart()
        {
            try
            {
                //Connected Clients
                this.clients = new ArrayList();

                //Initialize Socket
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
                
                //Initialize IPEndPoint for server, and listen on port 8888
                IPEndPoint serverIP = new IPEndPoint(IPAddress.Any, 8888);
                serverSocket.Bind(serverIP);

                //Initialize EndPoint for clients
                IPEndPoint clients = new IPEndPoint(IPAddress.Any, 8889);
                EndPoint EPSend = (EndPoint)clients;

                
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                serverSocket.Bind(serverIP);
                server.Listen(10);
                server.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
            
        }

        private static void AcceptCallback(IAsyncResult results)
        {
            client = server.EndAccept(results);

            Debug.Log("Connected");
        }

        private static void ReceiveCallback(IAsyncResult results)
        {
            Socket socket = (Socket)results.AsyncState;
            int rec = socket.EndReceive(results);

            pos = new float[rec / 4];
            Buffer.BlockCopy(buffer, 0, pos, 0, rec);

            Debug.Log("Received Cords - X: " + pos[0] + " Y: " + pos[1] + " Z: " + pos[2]);
        }

        void Start()
        {

        }

        void Update()
        {
            if (client != null)
            {
                client.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveCallback), client);
                cube.transform.position = new Vector3(pos[0], pos[1], pos[2]);
            }
            else
            {
                server.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
        }
    }
}
