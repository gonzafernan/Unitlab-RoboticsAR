/*
 * Autor: Gonzalo G. Fernández
 * Fecha: 01/11/2019
 * 
 * Archivo: AppData.cs
 * Versión 1.0
 */

using System;

public static class AppData
{
    /*
     * Clase que contiene toda la información estática
     * entre escenas de la aplicación. 
     * Por ejemplo, parámetros de la comunicación TCP/IP,
     * próxima escena a cargar, etc.
     */

    // Próxima escena a cargar
    private static int sceneToLoad = 1;

    // IP y PORT para comunicación TCP/IP
    private static String IP = "127.0.0.1";
    private static int PORT = 55001;

    //==================================================
    // Set-Get de parámetros anteriores
    public static int getSceneToLoad(){
        return sceneToLoad;
    }

    public static void setSceneToLoad(int newSceneToLoad){
        sceneToLoad = newSceneToLoad;
    }

    public static String getIP()
    {
        return IP;
    }

    public static void setIP(String newIP)
    {
        IP = newIP;
    }

    public static int getPORT()
    {
        return PORT;
    }

    public static void setPORT(int newPORT)
    {
        PORT = newPORT;
    }
}