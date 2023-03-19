using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using System.Threading;
using System.Net;
using System.Net.Sockets;

public class Client : MonoBehaviour
{

    public static GameObject cubeClient;
    private static Socket clientSocket;

    private static byte[] buffer = new byte[512];
    private static IPEndPoint remoteEP;

    //private static byte[] bpos;
    //private static float[] pos;

    //Vector3 prevPos = new Vector3(0f, 0f, 0f);

    //float currentTime = 0f;

    public static void StartClient()
    {
        try
        {
            IPAddress ip = IPAddress.Parse("192.168.2.58");
            remoteEP = new IPEndPoint(ip, 8889);

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //clientSocket.Connect(IPAddress.Parse(cubeClient.GetComponent<ServerInput>().getServerIP()), 8889);
            //Debug.Log("Connected to Server");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    void Start()
    {
        cubeClient = gameObject;
        StartClient();
    }

    void Update()
    {
        buffer = Encoding.ASCII.GetBytes((
            'x' + cubeClient.transform.position.x.ToString()
            + 'y' + cubeClient.transform.position.y.ToString()
            + 'z' + cubeClient.transform.position.z.ToString()).ToString());

        clientSocket.SendTo(buffer, remoteEP);

        Debug.Log("Coordinates being sent to server!\n"
            + "    X: " + cubeClient.transform.position.x
            + "    Y: " + cubeClient.transform.position.y
            + "    Z: " + cubeClient.transform.position.z);
    }
}
