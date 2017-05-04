using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static int score = 0;
    public static int lastScore;
    public string whatScoreToPrint = "actual";
    public Text scoreText;



    void Start()
    {
        score = 0;                            // Reinitialisation du score
        UpdateScore();
    }

    void Update()           
    {
        UpdateScore();      //mise a jour du score à chaque frame
    }

    void UpdateScore()
    {
        switch (whatScoreToPrint)
        {
            case "actual":
                scoreText.text = "Score : " + score;
                break;
            case "last":
                scoreText.text = "Score atteint : " + lastScore;
                break;
            case "best":
                scoreText.text = "Meilleur score : " + PlayerPrefs.GetInt("bestScore", 0);
                break;

        }
    }

    public static void SetScore(int value)
    {
        score += value;
    }

    public void ResetScore()
    {
        score = 0;
    }

}