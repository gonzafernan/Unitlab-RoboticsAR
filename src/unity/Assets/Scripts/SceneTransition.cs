/*
 * Autor: Gonzalo G. Fernández
 * Fecha: 01/11/2019
 * 
 * Archivo: SceneTransition.cs
 * Versión 1.0
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {
    /*
     * Clase con las rutinas necesarias a realizar
     * para coordinar la carga de escenas y
     * las diferentes animaciones que se ejecutan
     * al transicionar entre una escena y otra.
     */

    // Objeto Animator que posee las animaciones
    public Animator animator;

    // Atributo asociado a la rutina asíncrona de
    // carga de escena
    AsyncOperation asyncLoad;

    // Barra loading (Slider)
    public Slider loadingBar;

    public void LoadScene(){
        /*
         * Lectura de la próxima escena a cargar en
         * instancia de clase estática AppData.
         * Luego, se incia la rutina asíncrona que carga
         * los recursos necesarios para dicha escena.
         */
        int sceneToLoad = AppData.getSceneToLoad();

        // Cuando la lectura se realiza desde AppData,
        // se activa la barra de carga.
        StartCoroutine(LoadAsyncronously(sceneToLoad, true));
    }

    public void LoadScene(int sceneIndex){
        /*
         * Sobrecarga de la función LoadScene. Recibe como
         * parámetro el índice de la escena a cargar, y luego 
         * inicia la rutina asíncrona para cargar los recursos
         * necesarios.
         */

        // Cuando se recibe el índice de la escena a cargar,
        // se desactiva la barra de carga.
        StartCoroutine(LoadAsyncronously(sceneIndex, false));
    }

    IEnumerator LoadAsyncronously(int sceneIndex, bool loadBar){
        /*
         * Rutina asíncrona, que carga los recursos necesarios para
         * la próxima escena con índice sceneIndex.
         * El segundo parámetro loadBar habilita o no la barra de
         * carga gráfica.
         */
        asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        // No se permite la activación inmediata de la próxima
        // escena, ésto se activa cuando se da la señal de 
        // fin de animación.
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone){
            if (loadBar){
                // Si la barra de carga está activada, se analiza el 
                // progreso de carga
                float progress = Mathf.Clamp01(asyncLoad.progress / .9f);
                loadingBar.value = progress;
            }
            if (asyncLoad.progress >= 0.9f){
                // Cuando se finaliza la carga, se setea el Trigguer
                // para que comience la animación
                animator.SetTrigger("FadeOut");
            }

            yield return null; // Esperar el próximo cuadro antes de continuar
        }
    }

    void OnFadeComplete(){
        /*
         * Señal de fin de animación de FadeOut, por lo que se permite
         * la activación de la siguiente escena.
         */
        asyncLoad.allowSceneActivation = true;
    }
    
}
