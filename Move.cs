using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class Move : MonoBehaviour
{
    private int data;
    private int angleX;
    private int angleY;
    private int angleZ;
    private int accelX;
    private int accelY;
    private int accelZ;
    private int parser;
    private Vector3 previousAngle;
    private Vector3 currentAngle;
    private Vector3 difference;
    private Vector3 accel;
    public int accel_sense;
    public int position_scale;
    private int signed_accel_range;

    // Start is called before the first frame update
    void Start()
    {
        signed_accel_range  = 16 * (int)(9.81 * 10);
        Debug.Log(signed_accel_range);
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Settings");
            SceneManager.LoadScene("Settings");
        }
    }

    void OnMessageArrived(string msg)
    {
        if (msg != ""){
            // Debug.Log(msg);
            if(int.TryParse(msg, out parser)){
                data = int.Parse(msg);
            }

            if (data < 1300000){
                Debug.LogWarning("Input ignored");
            }else if(data < 1310000){
                accel_sense = data - 1300000; //range ±2 <-> ±16
                Debug.Log("Accelerometer sensitivity set to " + accel_sense + "G");
                signed_accel_range = accel_sense * (int)(9.81 * 10);
                Debug.Log("Signed data range: " + signed_accel_range);
            }else if(data < 1320000){
                accelX = data - 1310000 - signed_accel_range;
                accelY = 0;
                accelZ = 0;
                accel.Set(accelX,accelY,accelZ);
                transform.position += accel / position_scale;
            }else if(data < 1330000){
                accelY = data - 1320000 - signed_accel_range;
                accelX = 0;
                accelZ = 0;
                accel.Set(accelX,accelY,accelZ);
                transform.position += accel / position_scale;
            }else if(data < 1340000){
                accelZ = data - 1330000 - signed_accel_range;
                accelX = 0;
                accelY = 0;
                accel.Set(accelX,accelY,accelZ);
                transform.position += accel / position_scale;
            }else if(data < 1420000){
                angleX = data - 1410000;
            }else if(data < 1430000){
                angleY = data - 1420000;
            }else if(data < 1440000){
                angleZ = data - 1430000;
            }

            //Angle displacement
            currentAngle.Set(angleX,angleY,angleZ);
            difference = currentAngle - previousAngle;            
            transform.Rotate(difference);
            previousAngle = currentAngle;
        }
    }

    void OnConnectionEvent(bool success)
    {
        if(success){
            Debug.Log("Success");
        }else{
            Debug.Log("Failed");
        }
    }
}
