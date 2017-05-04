using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {

    public static int life = 100;
    public string gameOverScene = "GameOverGVR";
    public static Slider healthBar;
    public Text lifeText;


    void Start()
    {
        ResetLife();
        UpdateLife();
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Slider>();
    }
    private void Update()
    {
        UpdateLife();
        if (life <= 0)
            OnDeath();
    }


    //Diminue la vie et rafraichie l affichage de la bar de vie
    void UpdateLife()
    {
        if (lifeText != null)
            lifeText.text = "" + life;
    }

    //Mise a jour de l affichage de la vie
    void ResetLife()
    {
        life = 100;
    }

    public static void LowerLife(int amount)
    {
        life -= amount;
        healthBar.value -= amount;
    }



    //Que faire à la mort du joueur ?
    void OnDeath()
    {
        Scoring.lastScore = Scoring.score;
        gameObject.GetComponent<ScenesManager>().GoToScene(gameOverScene);
        if (PlayerPrefs.GetInt("bestScore", 0) < Scoring.lastScore)                 //pour enregistrer le meilleur score    
            PlayerPrefs.SetInt("bestScore", Scoring.lastScore);
    }
}
