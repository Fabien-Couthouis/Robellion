using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Déplacement selon les collisions : à chaque collision l'objet se tourne vers une direction aléatoire puis il est attiré vers le joueur 
// dans un rayon < detectionRadius

public class CubeControllerBlue : Enemy
{


    // Update is called once per frame
    void Update()
    {
        if (inDetectionRadius())
        {                               //si l objet arrive dans le rayon de détection, faire action particulière
            whenInDetectionRadius();
            Quaternion.LookRotation(Target.transform.position);
        }
        else                                                                   //Déplacement selon les collisions si hors du rayon de détection
            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
        RandomRotation();
    }

    //Déplacement selon les collisions : à chaque collision l'objet se tourne vers une direction aléatoire
    public void RandomRotation()
    {
        if (!inDetectionRadius() && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out Hit, 7))
            transform.Rotate(Vector3.up * Random.Range(50, 200));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
            onPlayer();
    }

}