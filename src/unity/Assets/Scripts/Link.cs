/*
 * Autor: Gonzalo G. Fernández
 * Fecha: 01/11/2019
 * 
 * Archivo: Link.cs
 * Versión 1.0
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link {
    /*
     * La clase Link busca ser similar a la clase Link del toolbox para 
     * MATLAB de Peter Corke, y poseer la descripción de Denavit Hartenberg
     * de el eslabón correspondiente.
     */

    // Parámetros Denavit Hartenberg
    public float theta, d, alpha, a;
    private bool sigma;

    // Link asociado al GameObject en escena
    public GameObject linkObj;
    
    // Matriz de transformación homogénea
    public Matrix4x4 T;

    public Link(float theta, float d, float a, float alpha, bool sigma, GameObject obj){
        this.theta = theta;
        this.d = d;
        this.a = a;
        this.alpha = alpha;
        this.sigma = sigma;

        this.linkObj = obj;
    }

    public Matrix4x4 getT(){
        /*
         *  In Unity the Z axis is the Y, and the X axis is the Z. 
         */
        
        Quaternion trotY = Quaternion.Euler(0, this.theta*Mathf.Rad2Deg, 0);
        Vector3 translY = new Vector3(0, this.d, 0);
        Vector3 translZ = new Vector3(0, 0, -this.a);
        Quaternion trotZ = Quaternion.Euler(0, 0, -this.alpha*Mathf.Rad2Deg);

        this.T = Matrix4x4.Rotate(trotY) * Matrix4x4.Translate(translY) * Matrix4x4.Translate(translZ) * Matrix4x4.Rotate(trotZ);
        
        return this.T;
    }
}
