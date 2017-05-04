using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC : MonoBehaviour
{
    public static int scoreMultiplier = 1;
    public static int lifeDivider = 1;
    public RaycastHit Hit;
    public float health = 2f;

    public int xradiusmax = 50;
    public int yradiusmax = 50;
    public int zradiusmax = 50;
    public int xradiusmin = 15;
    public int yradiusmin = 0;
    public int zradiusmin = 15;

    //Donne une position aléatoire dans le rayon défini autour du joueur
    public Vector3 RandomPosition(int xradiusmax, int yradiusmax, int zradiusmax)
    {
        int posx = Random.Range(-xradiusmax, xradiusmax);
        int posy = Random.Range(-yradiusmax, yradiusmax);
        int posz = Random.Range(-zradiusmax, zradiusmax);
        return (new Vector3(posx, posy, posz) + Camera.main.gameObject.transform.position);
    }

    //spawn aléatoire, à une distance de la cible située entre radiusmin et radiusmax
    public void RandomSpawn()
    {
        Vector3 position = RandomPosition(xradiusmax - xradiusmin, yradiusmax - yradiusmin, zradiusmax - zradiusmin) + new Vector3(xradiusmin, yradiusmin, zradiusmin);
        Instantiate(gameObject, position, Quaternion.identity);
    }


}
