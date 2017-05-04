using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Activer ou non la VR sur une scene (pour les cardboards ainsi) / changer la couleur du laser en jeu / utiliser le touchpad du GVR au lieu de la visée automatique pour le laser
public class Options : MonoBehaviour

{
    public bool activateVR = true;
    public GameObject LaserPreview;


    //Activer ou non la VR
    void Start()
    {
        UnityEngine.VR.VRSettings.enabled = activateVR;
    }

    //Utiliser le pavé tactile du GVR
    public void activateTouchpad()
    {
        Gazing.touchpadActivated = true;
    }


    //Changer la couleur du laser en jeu, depuis les options du menu principal

    public void SetRed(float value)
    {
        OnValueChanged(value, 0);
    }

    public void SetGreen(float value)
    {
        OnValueChanged(value, 1);
    }

    public void SetBlue(float value)
    {
        OnValueChanged(value, 2);
    }

    //Change les couleurs de tout ce qui est necessaire (aperçu + lineRenderer du laser en jeu).
    //Les differents getcomponents sont la au cas ou on veuille appliquer la couleur du laser à un objet dans le futur.
    public void OnValueChanged(float value, int channel)
    {
        Color c = Color.green;

        if (LaserPreview.GetComponent<SpriteRenderer>() != null)
            c = LaserPreview.GetComponent<SpriteRenderer>().color;
        else if (LaserPreview.GetComponent<Light>() != null)
            c = LaserPreview.GetComponent<Light>().color;
        else if (LaserPreview.GetComponent<Image>() != null)
            c = LaserPreview.GetComponent<Image>().color;
        else if (LaserPreview.GetComponent<Renderer>() != null)
            c = LaserPreview.GetComponent<Renderer>().material.color;


        c[channel] = value;

        if (LaserPreview.GetComponent<SpriteRenderer>() != null)
            LaserPreview.GetComponent<SpriteRenderer>().color = c;
        else if (LaserPreview.GetComponent<Light>() != null)
            LaserPreview.GetComponent<Light>().color = c;
        else if (LaserPreview.GetComponent<Image>() != null)
            LaserPreview.GetComponent<Image>().color = c;
        else if (LaserPreview.GetComponent<Renderer>() != null)
            LaserPreview.GetComponent<Renderer>().material.color = c;

        Laser.LaserColor = c;
    }


}
