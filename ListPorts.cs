using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO.Ports;
using TMPro;
using UnityEngine.SceneManagement;
using System.Reflection.Emit;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;

public class ListPorts : MonoBehaviour
{
    public TMP_Dropdown PortsDropdown;
    public TMP_InputField BaudRate;
    private List<string> _ports;
    public GameObject prefab;
    private int parser;

    void Start()
    {
        RefreshPortsDropdown();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    public void RefreshPortsDropdown()
    {
        // Remove all the previous options
        PortsDropdown.ClearOptions();

        // Get port names
        string[] portNames = SerialPort.GetPortNames();
        _ports = portNames.ToList();

        // Add the port names to our options
        PortsDropdown.AddOptions(_ports);
    }

    public void EnterDemo(){
        prefab.GetComponent<SerialController>().portName = PortsDropdown.options[PortsDropdown.value].text;
        if(int.TryParse(BaudRate.text, out parser)){
          prefab.GetComponent<SerialController>().baudRate = int.Parse(BaudRate.text);
        }else{
            Debug.Log("Invalid baud rate");
            return;
        }
        SceneManager.LoadScene("Demo");
    }
}

