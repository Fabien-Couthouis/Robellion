using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gazing : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
{

    public static bool touchpadActivated;    //Si la variable est vraie, utiliser le pavé tactile/clic souris pour activer le laser, sinon laisser la visée auto (par défaut)
    public bool GazedAt;           //objet visé ou non
    public float StaringTime;
    private float relativeHealth;

    public Laser Laser;

    // Use this for initialization
    void Start () {
        Laser = GameObject.FindWithTag("LaserTag").GetComponent<Laser>();  //Recherche du laser sur la scene 
    }
	
	// Update is called once per frame
	void Update () {
        relativeHealth = gameObject.GetComponent<NPC>().health / NPC.lifeDivider;

        if (GazedAt)
        {
            if (!touchpadActivated)
                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            if (Laser.isActivated == true)
                StaringTime += Time.deltaTime;
        }

        //Lorsque plus de vie
        if (StaringTime >= relativeHealth)
        {
            ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
            if (GetComponent<Enemy>() != null)
                GetComponent<Enemy>().WhenDestroyed();
            else if (GetComponent<Bonus>() != null)
                GetComponent<Bonus>().WhenDestroyed();
        }     
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        StopLaser();
        //Debug.Log("exit");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GazedAt = true;
        //Debug.Log("enter");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Laser.TirLaser();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!touchpadActivated)
            StopLaser();
        //Debug.Log("exit");
    }


    public void StopLaser()
    {
        Laser.ArretLaser();
        GazedAt = false;
    }

}

