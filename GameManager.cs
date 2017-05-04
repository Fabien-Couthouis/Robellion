using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityStandardAssets.ImageEffects;



public class GameManager : MonoBehaviour
{

    #region Variables

    //Gestion de la pause
    public GameObject pauseMenu;
    private bool isPaused = false;

    //Ennemis
    public float enemySpawnTime = 3f;
    public Enemy[] enemiesPrefabs;    //Placer les prefabs sur Unity
    private float probaMax = 0;

    //Bonus
    public Bonus[] bonusPrefabs;             //        

    //Laser
    private Laser laser;
    
    #endregion


    #region Start
    // Use this for initialization
    void Start()
    {
        DynamicGI.UpdateEnvironment();
        //player = GameObject.FindWithTag("MainCamera");
        laser = GameObject.FindWithTag("LaserTag").GetComponent<Laser>();
        foreach (Enemy enemy in enemiesPrefabs)
            probaMax += enemy.spawnProba;
        StartCoroutine("SpawnEnemies");
        StartCoroutine("SpawnBonus");     
    }


    #endregion


    #region Gestion de la pause
    //  Pause/resume à chaque appel
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }


    public void TogglePauseMenu()
    {
        TogglePause();
        if (isPaused)
            pauseMenu.SetActive(true);
        else
            pauseMenu.SetActive(false);
    }
    #endregion


    #region Coroutines
    //Invoque un ennemi aléatoire, en fonction de sa probabilité de spawn, tous les "enemySpawnTime"
    IEnumerator SpawnEnemies()
    {
        for (;;)
        {
            float p = 0;
            float random = Random.Range(0, probaMax);
            foreach (Enemy enemy in enemiesPrefabs)
            {
                if (p <= random && random < p + enemy.spawnProba)
                    enemy.RandomSpawn();
                p += enemy.spawnProba;
            }
            yield return new WaitForSeconds(enemySpawnTime);
        }
    }

    //Invoque un bonus aléatoire toutes les 15-30s 
    IEnumerator SpawnBonus()
    {
        for (;;)
        {
            int random = Random.Range(0, bonusPrefabs.Length);                //même probabilité de spawn pour chaque bonus
            bonusPrefabs[random].RandomSpawn();
            yield return new WaitForSeconds(Random.Range(15, 30));           //spawn de bonus entre 15 et 30s 
        }
    }

    //Les coroutines des Bonus :

    IEnumerator BonusTime()
    {
        for (;;)
        {
            Time.timeScale = 0.5f;
            NPC.lifeDivider = 2;                 //les ennemis mettent autant à être détruits dans la réalité
            yield return new WaitForSeconds(15);
            Debug.Log("StopBonusSpeed");
            Time.timeScale = 1f;
            NPC.lifeDivider = 1;
            yield break;
        }
    }

    IEnumerator BonusScore()
    {
        for (;;)
        {
            NPC.scoreMultiplier = 2; 
            yield return new WaitForSeconds(15);
            Debug.Log("StopBonusScore");
            NPC.scoreMultiplier = 1;
            yield break;
        }
    }

    IEnumerator BonusLaser()
    {
        for (;;)
        {
            NPC.lifeDivider = 2;
            laser.RainbowLaser();      //gros laser
            yield return new WaitForSeconds(15);
            Debug.Log("StopBonusLaser");
            NPC.lifeDivider = 1;
            laser.StopRainbowLaser();
            yield break;

        }

    }
    #endregion


}









// Trucs qui peuvent servir pour mode histoire
//Scoring et fin du jeu


//public Object effect;
//public int wantedScore = 100;
//private bool startCount = false;
//private float startTime = 0.0f;

//void Update()
//{
//    if (Scoring.score == wantedScore && !startCount)     //dernière condition pour éviter l execution à chaque frame après l atteinte du score
//    {
//        InvokeRepeating("lightning", 0, 0.4f);     //spawn d un ballet lumineux toutes les 0.4s
//        startCount = true;
//        startTime = Time.time;
//    }
//if (startCount)       //dès que le score voulu a été atteint, commence à compter pour changer de scène 5s après
//{
//    if (startTime + 5.0f < Time.time)
//        SceneManager.LoadScene("Scene2");
//}
//}

//Permet le spawn d un ballet lumineux + effet Twirl
//void lightning()           
//{
//    //CancelInvoke("Spawn"); marche pas   
//    Instantiate(effect, player.transform.position + new Vector3(0, 1, 0), player.transform.rotation);
//    (Camera.main.GetComponent<Twirl>()).enabled = true;
//}
