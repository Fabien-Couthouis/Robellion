using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : NPC
{

    public string typeOfBonus;
    private int lifetime = 20;
    private GameManager manager;

    private void Start()
    {
        Destroy(gameObject, lifetime);              // ce bonus s'autodetruira dans 20 secondes
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();  
    }
    // Use this for initialization
    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * 80, 0));  //ça tourne !
    }

    public void WhenDestroyed()
    {
        ActivateBonus();
        Destroy(gameObject);
    }


    public void ActivateBonus()
    {
        switch (typeOfBonus)
        {
            case "time":
                manager.StartCoroutine("BonusTime");     //ralentis le temps 
                break;
            case "score":
                manager.StartCoroutine("BonusScore");       //score multiplier
                break;
            case "laser":
                manager.StartCoroutine("BonusLaser");      //gros laser
                break;
        }
    }

}
