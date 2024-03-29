using System.Collections;
using System.Collections.Generic;
using Quaternion = UnityEngine.Quaternion;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;
using Unity.VisualScripting;
using System;

public class Move : MonoBehaviour
{
    private int payload;
    private float data;
    private float sign;
    private float QuatW;
    private float QuatX;
    private float QuatY;
    private float QuatZ;
    private int accelX;
    private int accelY;
    private int accelZ;
    private int parser;
    private Quaternion target;
    private Vector3 change;
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
                payload = int.Parse(msg);
            }  
            if (payload > 0){
                sign = 1;
            }else if(payload < 0){
                sign = -1;
            }else{
                sign = 0;
            }
            data = Mathf.Abs(payload);

            if (data < 1310000){
                Debug.Log(data);
                Debug.LogWarning("Input ignored");
            }
            // else if(data < 1320000){
            //     accelX = data - 1310000;
            //     accelY = 0;
            //     accelZ = 0;
            //     accel.Set(accelX,accelY,accelZ);
            //     transform.position += accel / position_scale;
            // }else if(data < 1330000){
            //     accelY = data - 1320000;
            //     accelX = 0;
            //     accelZ = 0;
            //     accel.Set(accelX,accelY,accelZ);
            //     transform.position += accel / position_scale;
            // }else if(data < 1340000){
            //     accelZ = data - 1330000;
            //     accelX = 0;
            //     accelY = 0;
            //     accel.Set(accelX,accelY,accelZ);
            //     transform.position += accel / position_scale;
            // }
            else if(data < 1420000){
                QuatW = (data - 1410000) / (float)1000.00;
            }else if(data < 1430000){
                QuatX = (data - 1420000) / (float)1000.00 * sign;
            }else if(data < 1440000){
                QuatY = (data - 1430000) / (float)1000.00 * sign;
            }else if(data < 1450000){
                QuatZ = (data - 1440000) / (float)1000.00 * sign;
            }

            //Angle displacement
            target.w = QuatW;
            target.x = QuatX;
            target.y = QuatZ; //Y-up system
            target.z = QuatY;

            Debug.Log(target);

            transform.rotation = target;
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
