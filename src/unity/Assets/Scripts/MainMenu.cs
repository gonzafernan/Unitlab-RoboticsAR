/*
 * Autor: Gonzalo G. Fernández
 * Fecha: 01/11/2019
 * 
 * Archivo: MainMenu.cs
 * Versión 1.0
 */

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject sceneTransition;

    GameObject connectionToggle, inputIP, inputPort;

    private void Start(){
        connectionToggle = GameObject.Find("EnableConnection");
        inputIP = GameObject.Find("InputIP");
        inputPort = GameObject.Find("InputPort");
    }

    public void EnableConnection(){
        bool state = connectionToggle.GetComponent<Toggle>().isOn;
        if (state){
            //animator.SetTrigger("EnabledConnection");
            Debug.Log("Connection mode enabled.");
            AppData.setSceneToLoad(2);
        } else {
            //animator.SetTrigger("DisabledConnection");
            AppData.setSceneToLoad(3);
            Debug.Log("Connection mode disabled.");
        }
    }

    public void EditedIP(){
        String editedIP = inputIP.GetComponent<InputField>().text;
        AppData.setIP(editedIP);
        Debug.Log("IP changed to: " + editedIP);
    }

    public void EditedPort(){
        int editedPort = int.Parse(inputPort.GetComponent<InputField>().text);
        AppData.setPORT(editedPort);
        Debug.Log("PORT changed to: " + editedPort);
    }

    public void PlayScene(){
        sceneTransition.GetComponent<SceneTransition>().LoadScene(0);
    }

    public void BackScene(){
        AppData.setSceneToLoad(1);
        sceneTransition.GetComponent<SceneTransition>().LoadScene(0);
    }

    public void QuitApp(){
        Debug.Log("Quitting...");
        Application.Quit();
    }
    
}
