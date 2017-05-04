using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Déplacement vers l'ennemi le plus proche pour lui augmenter sa vitesse de la valeur "speedBoost". Meurt si aucun ennemie n est proche 
public class CubeControllerGreen : Enemy {
    public float speedBoost = 3f;
    private int layerMask = 1 << 8;   //permet de ne detecter que les objets marqué du layer 8 : Enemy


    // Use this for initialization
    void Start()
    {
        if (new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyTag")).Count == 0)        //si pas d autres enemies sur la scene, destruction de cet objet
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)      //si il y a un enemi à suivre, le suivre
            MoveToTarget();

        else                //sinon : chercher un nouvel enemi à suivre, s'il existe. 
        {
            if ((new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyTag"))).Count == 0)        //si pas d autres enemies sur la scene, destruction de cet objet
                Destroy(gameObject);
            else

            {
                int radius = 15;
                while ( (FindClosestEnemy(radius) == null)  )//&& radius < 200)             //on cherche l ennemi le plus proche en balayant des rayons de plus en plus grands
                    radius += 15;
                Target = FindClosestEnemy(radius);                     // on a trouvé l ennemi le plus proche ...
                if (Target == null)
                    Destroy(gameObject);
                Target.GetComponent<Enemy>().m_speed += speedBoost;   // auquel on applique le boost de vitesse
            }
        }
    }


    //Retourne l'ennemi le plus proche dans un rayon donné
    private GameObject FindClosestEnemy(int radius)
    {
        Collider[] nearEnemies = Physics.OverlapSphere(transform.position, radius, layerMask);     //liste des enemis situés dans le rayon
        Collider closestEnemy = null;
        foreach (Collider hit in nearEnemies)
        {
            if (hit.tag == "EnemyTag")
            {
                if (closestEnemy == null)
                    closestEnemy = hit;
                if (Vector3.Distance(transform.position, hit.transform.position) < Vector3.Distance(transform.position, closestEnemy.transform.position)) //calcul de l enemi le plus proche parmi ceux dans le rayon
                    closestEnemy = hit;
            }
        }
        if (closestEnemy != null)
                return closestEnemy.gameObject;
        else return null;
        
    }
}
