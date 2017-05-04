using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : NPC
{
    public float m_speed = 0.2f;
    public float detectionRadius = 15f;
    public GameObject Target;
    public float spawnProba = 0.25f;
    public int scoreValue = 10;
    public int damages = 20;
    public AudioSource deathSound;
    
    private void Start()
    {
        deathSound = Camera.main.gameObject.GetComponent<AudioSource>();
        if (this.tag == "EnemyTag")
            Target = Camera.main.gameObject;
    }

    public void WhenDestroyed()
    {
        Destroy(gameObject);
        Scoring.SetScore(scoreValue * scoreMultiplier);
        deathSound.Play();
    }

    //L'objet est-il dans le rayon de détection ?
    public bool inDetectionRadius()  
    {
        if (Vector3.Distance(transform.position, Camera.main.gameObject.transform.position) < detectionRadius)
            return true;
        else return false;
    }

    //Que faire lorque l'objet arrive dans le rayon de détection
    public void whenInDetectionRadius()
    {
        //Destroy(gameObject.GetComponent<Rigidbody>());
        MoveToTarget();
        //MoveToTarget();
    }



    //déplacement vers la cible
    public void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, m_speed * Time.deltaTime);
    }


    //action à effectuer lorsque l'ennemi est sur le joueur
    public void onPlayer()
    {
        gameObject.GetComponent<Gazing>().StopLaser();     //pour arreter l affichage du laser
        Life.LowerLife(damages);
        deathSound.Play();
        Destroy(gameObject);
    }
}



