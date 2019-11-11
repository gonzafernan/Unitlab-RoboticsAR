/*
 * Autor: Gonzalo G. Fernández
 * Fecha: 01/11/2019
 * 
 * Archivo: readSocket.cs
 * Versión 1.0
 */

using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;


public class readSocket : MonoBehaviour {
    // Use this for initialization
    TcpListener listener;
    String msg, IP = "127.0.0.1";
    int PORT = 55001;

    public GameObject robotArm;
    
    void Start(){
        IP = AppData.getIP();
        PORT = AppData.getPORT();
        listener = new TcpListener(IPAddress.Parse(IP), PORT);
        listener.Start();
        print("TCP/IP Communication ready - IP:" + IP + " PORT:" + PORT);
    }
    // Update is called once per frame
    void Update(){
        if (!listener.Pending()){
        } else {
            //print("socket comes");
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            msg = reader.ReadToEnd();
            print(msg);
            msgParser(msg);
        }
    }

    void msgParser(String msg){
        if (msg[0] != ':'){
            Debug.Log("Error: Invalid msg");
            return;
        }
        String[] subq = msg.Split(':');
        for (int i=1; i<subq.Length; i++){
            robotArm.GetComponent<SerialLink>().q[i-1] = float.Parse(subq[i]);
            Debug.Log("Q" + i + ": " + subq[i]);
        }
    }
}
