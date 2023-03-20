using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class UnityClient : MonoBehaviour
{
    private UdpClient UDPClient;
    private IPEndPoint serverEndPoint;
    private bool connected = false;
    private Queue<string> positionUpdateQueue = new Queue<string>();

    public int UDPPort = 8889;
    public InputField serverIPInputField;
    public Button connectButton;

    private void Start()
    {
        UDPClient = new UdpClient();
        connectButton.onClick.AddListener(ConnectToServer);
    }

    private async void ConnectToServer()
    {
        //Server IP from input field
        string serverIP = serverIPInputField.text;
        if (string.IsNullOrEmpty(serverIP))
        {
            Debug.Log("Please enter a valid server IP");
            return;
        }

        //Initialize client and connect to server now
        serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), UDPPort);
        Debug.Log("Connecting to server...");
        UDPClient.Connect(serverEndPoint);

        connected = true;
        Debug.Log($"Connected to server {serverEndPoint}!");

        //Start sending and receiving position updates to/from server
        await SendPositionUpdates();
        await ReceivePositionUpdates();
    }

    private async Task SendPositionUpdates()
    {
        while (connected)
        {
            if (this == null)
            {
                return;
            }

            if (positionUpdateQueue.Count > 0)
            {
                //Send message to server
                string posMsg = positionUpdateQueue.Dequeue();
                byte[] msgBytes = Encoding.ASCII.GetBytes(posMsg);
                await UDPClient.SendAsync(msgBytes, msgBytes.Length);
                Debug.Log("You sent some data!");
            }


            //Wait for short time before sending next update
            await Task.Delay(16);
        }
    }

    private async Task ReceivePositionUpdates()
    {
        while (connected)
        {
            if (this == null)
            {
                return;
            }

            //Receiving msg from server
            UdpReceiveResult result = await UDPClient.ReceiveAsync();

            byte[] msgBytes = result.Buffer;

            //Convert msg to string
            string msg = Encoding.ASCII.GetString(msgBytes);

            //Print receive message to console
            Debug.Log($"Received position update to server: {msg}");
        }
    }

    private void Update()
    {
        if (connected)
        {
            //Add position updates to queue
            string posMsg = transform.position.x + "," + transform.position.y + "," + transform.position.z;
            positionUpdateQueue.Enqueue(posMsg);
        }
    }
    private void OnDestroy()
    {
        connected = false;
        UDPClient.Close();
    }
}
