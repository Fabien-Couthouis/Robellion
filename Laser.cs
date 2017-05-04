using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Laser : MonoBehaviour
{

    public static Color LaserColor = Color.green;
    public Material RainbowMaterial;
    private Material OriginalLaserMaterial;    
    private LineRenderer liner;
    public bool isActivated;

   

	// Use this for initialization
	void  Start  () {
        transform.GetChild(0).gameObject.SetActive(false);    //Les étincelles sont dans un objet enfant attaché au laser, on ne veut pas les activer sans raison
        liner =  gameObject.GetComponent<LineRenderer>();     //On affecte le LineRenderer attaché à notre caméra à l'instance liner
        OriginalLaserMaterial = liner.material;
        liner.startColor = LaserColor;
        liner.enabled = false;                              // On désactive le LineRenderer au démarrage pour éviter de tirer en continu           
    }


    void Update ()
    {
        if (Input.GetMouseButtonDown(0) && Gazing.touchpadActivated)
        {
            TirLaser();
        }

        if (Input.GetMouseButtonUp(0) && Gazing.touchpadActivated)
        {
            ArretLaser();
        }
    }



    public void TirLaser() //Exécuter le tir du laser sur plusieurs frames
    {
        liner.enabled = true;
        isActivated = true;
        Ray ray = new Ray(transform.position, transform.forward); // création d'un rayon vers l'avant et partant de la position de la caméra tant qu'un ennemi est sélectionné
        RaycastHit hit;                                      //Point d'impact du raycast avec un obstacle sur sa trajectoire
        liner.SetPosition(0, ray.origin);                   //Le LineRenderer part de la caméra
        if (Physics.Raycast(ray, out hit, 100)) {
            liner.SetPosition(1, hit.point);               // Et s'arrête au point d'impact
            transform.GetChild(0).gameObject.SetActive(true);                    //on active ici les étincelles (olalala c est joliiiiiiii)
            transform.GetChild(0).gameObject.transform.position = hit.point;     // et on les met à l'emplacement du point d impact 
        }
        else
            liner.SetPosition(1, ray.GetPoint(100));
    }


    //Permet d'arrêter le tir du laser
    public void ArretLaser() 
    {
        transform.GetChild(0).gameObject.SetActive(false);
        liner.enabled = false;
        isActivated = false;
    }


       
    public void RainbowLaser()
    {
        liner.startColor= Color.white;            //on passe la couleur du liner en blanc pour obtenir l effet rainbow
        liner.material = RainbowMaterial;         // on applique le material désiré
        if (liner.enabled)
            liner.material.mainTextureOffset = new Vector2(0, Time.time);      //et on tourne !
    }    
        
    public void StopRainbowLaser()
    {
        liner.material = OriginalLaserMaterial;        //rétablissement des parametres par defaut
        liner.startColor = LaserColor;
        Debug.Log("StopRainbow");
        if (liner.enabled)
            liner.material.mainTextureOffset = new Vector2(0, 0);
    }
        

    }



