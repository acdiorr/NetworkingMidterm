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
        private static IPAddress address;
        public static int TCPPort = 8888;
        public static int UDPPort = 8889;

        private static IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());
        private static IPEndPoint TCPEndPoint;
        public static EndPoint UDPEndPoint;
        private static Socket TCPSocket;
        private static Socket UDPSocket;
        private static Socket client;

        private static byte[] buffer = new byte[512];
        private static byte[] outBufer = new byte[512];
        private static float[] pos = { 0f, 0f, 0f };

        static void Main(string[] args)
        {
            Console.WriteLine("Server starting...");

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            //Connected Clients
            //this.clients = new ArrayList();

            //Initialize Socket
            UDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //Initialize IPEndPoint for server, and listen on port 8889
            UDPEndPoint = new IPEndPoint(IPAddress.Any, 8889);

            //Non-blocking
            UDPSocket.Blocking = false;

            UDPSocket.Bind(UDPEndPoint);

            //Initialize EndPoint for clients
            IPEndPoint clients = new IPEndPoint(IPAddress.Any, 8889);
            EndPoint EPSend = (EndPoint)clients;

            UDPSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult results)
        {
            client = UDPSocket.EndAccept(results);

            Console.WriteLine("Connected");
        }

        private static void ReceiveCallback(IAsyncResult results)
        {
            Socket socket = (Socket)results.AsyncState;
            int rec = socket.EndReceive(results);

            pos = new float[rec / 4];
            Buffer.BlockCopy(buffer, 0, pos, 0, rec);

            Console.WriteLine("Received Cords - X: " + pos[0] + " Y: " + pos[1] + " Z: " + pos[2]);
        }

        void Start()
        {

        }

        void Update()
        {
            
        }
    }
}
