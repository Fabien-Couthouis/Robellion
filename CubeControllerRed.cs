using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Déplacement aléatoire dans le rayon défini, est attiré par le joueur si arrive dans un rayon < detectionRadius
public class CubeControllerRed : Enemy{
    public Vector3 direction = Vector3.zero;
    private bool collisionDetected;



    void Update () {                                          
        if (inDetectionRadius())
        {                               
            whenInDetectionRadius();
            Quaternion.LookRotation(-Target.transform.position);
        }
        else                                                      
            MoveToRandomPosition(ref direction);
    }


    //Déplace l'objet à une position aléatoire dans le rayon défini, avec gestion des collisions et rotation
    public void MoveToRandomPosition(ref Vector3 direction)
    {
        if (collisionDetected)
        {  //si collision, on reprend une nouvelle direction
            direction = Vector3.zero;
            collisionDetected = false;
        }

        if (transform.position == direction || direction == Vector3.zero)
        {
            direction = RandomPosition(xradiusmax, yradiusmax, zradiusmax);                                                                                    
            xradiusmax -= 5;    //se rapproche petit à petit du joueur ...
            zradiusmax -= 5;    //
        }

        transform.rotation = Quaternion.LookRotation(-direction);                                              //se tourner ...
        transform.position = Vector3.MoveTowards(transform.position, direction, m_speed * Time.deltaTime);    //et aller vers la nouvelle direction
    } 


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
            onPlayer();
        else
            collisionDetected = true;
    }


}
