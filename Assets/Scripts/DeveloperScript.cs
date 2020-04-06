using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeveloperScript : MonoBehaviour
{
    public Canvas dev_console;
    public ArrayList logs = new ArrayList();
    public string output = "";
    public string stack = "";
    public string full_log = "";


    public TextMeshProUGUI console_log;

    void Start()
    {


        Application.logMessageReceived += HandleLog;
        Debug.Log("se");


        Debug.Log("is this logged too?");
        this.enabled = true;
    }

    // Log to dev command
    void HandleLog(string log,string stacktrace,LogType type)
    {
        output = log;
        stack = stacktrace;
        full_log = output + " | " +stack;
        console_log.SetText (console_log.text + full_log + "\n \n \n \n ");
    }


    // Show FPS 
    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), ""+(1.0f / Time.smoothDeltaTime));

    }

}
