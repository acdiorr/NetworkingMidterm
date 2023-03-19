using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OverlayUpdate : MonoBehaviour
{
    private Text myText;
    private static float position;


    public static void UpdatePos(float pos)
    {
        position = pos;
    }

    void Start()
    {
        myText = GetComponent<Text>();        
    }

    void Update()
    {
        Debug.Log(position);
        myText.text = "Position X: " + position;    
    }
}
