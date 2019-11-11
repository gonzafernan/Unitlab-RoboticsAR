using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SerialLink : MonoBehaviour {
    /*
     * La clase SerialLink busca ser similar a la clase principal
     * SerialLink del toolbox para MATLAB de Peter Corke, y poseer
     * la información necesaria para manipular la simulación del 
     * robot.
     */

    // Objeto con información teórica idem toolbox de
    // Peter Corke.
    public Link[] link;

    // Links a los modelos 3d en escena
    private GameObject[] linkObject;

    // Denavit Hartenberg parameter for SCARA IRB
    private float a1=0.2f, a2=0.25f, alpha4=0;
    public float[] q;
    

    void Start(){
        /*
         * El método Start se ejecuta en la instanciación
         * de la clase, previo al primer cuadro Update.
         */

        // Número de eslabones sin contar la base
        int nLinks = transform.childCount;

        // Inialización del vector de variables articulares
        q = new float[nLinks];
        for (int i=0; i<nLinks; i++){
            q[i] = 0;
        }

        // Linkeo de los modelos 3d en escena con los linkObject
        linkObject = new GameObject[nLinks];
        for (int i=0; i<nLinks; i++){
            linkObject[i] = transform.GetChild(i).gameObject;
        }

        // Inialización de objetos Link similar a la clase Link 
        // del toolbox para MATLAB, con la descripción
        // de Denavit-Hartenberg
        link = new Link[nLinks];
        link[0] = new Link(q[0], 0, a1, 0, false, linkObject[1]);
        link[1] = new Link(q[1], 0, a2, 0, false, linkObject[2]);
        link[2] = new Link(0, q[2], 0, 0, true, linkObject[3]);
        link[3] = new Link(q[3], 0, 0, alpha4, false, linkObject[3]);

        updateConfig();
    }
    

    void Update(){
        // El método Update se ejecuta una vez por cuadro.
        updateDH();
        updateConfig();
    }

    private void updateConfig(){
        /*
         * Método que actualiza los objetos link en base a la posición, 
         * escala y orientación del robot simulado. Se utilizan los métodos
         * implementados en la clase MatrixExtension.
         */
        Matrix4x4 aux;
        for (int i=0; i<link.Length; i++){
            aux = linkObject[i].transform.localToWorldMatrix * link[i].getT();
            link[i].linkObj.transform.localScale = aux.ExtractScale();
            link[i].linkObj.transform.rotation = aux.ExtractRotation();
            link[i].linkObj.transform.position = aux.ExtractPosition();
        }
    }

    private void updateDH(){
        /*
         * Actualización de los parámetros variables en la descripción DH,
         * en los objetos link en base a la configuración articular actual.
         */
        link[0].theta = q[0];
        link[1].theta = q[1];
        link[2].d = q[2];
        link[3].theta = q[3];
    }

    public void setSliderQ(){
        /*
         * Método específico del modo manual de la aplicación, asociado a un determinado 
         * Slider, y se actualiza el q correspondiente en base al valor ingresado
         * por el usuario.
         */
        for (int i=1; i<=q.Length; i++){
            float angle = GameObject.Find("SliderQ" + i).GetComponent<Slider>().value;
            this.q[i-1] = angle * Mathf.Deg2Rad;
        }
    }
}
