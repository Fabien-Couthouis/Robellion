using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Déplacement en ligne droite vers l'objet indiqué 
 
public class CubeControllerYellow : Enemy {

    // Update is called once per frame
    void Update () {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x, -44, Target.transform.position.z), m_speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(Target.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
            onPlayer();
    }
}
