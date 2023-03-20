using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ServerInput : MonoBehaviour
{
    public static ServerInput instance;

    public string serverIP;
    public GameObject inputText;

    public GameObject displayMsg;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object...");
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        //inputText.interactable = false;
    }

    public void SetServerIP()
    {
        serverIP = inputText.GetComponent<Text>().text;

        displayMsg.GetComponent<Text>().text = "Server IP: " + serverIP;
    }

    public string getServerIP()
    {
        return serverIP;
    }
}
