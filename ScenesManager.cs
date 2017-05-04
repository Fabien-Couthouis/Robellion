using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

    public void GoToScene(string scene = "MainMenu")
    {
        Time.timeScale = 1.0f;                              //remet le temps à 1 (pour éviter les erreurs en cas de pause)
        SceneManager.LoadScene(scene);                     //entrer le nom de la scene dans Unity (sans guillemet)
    }
}
