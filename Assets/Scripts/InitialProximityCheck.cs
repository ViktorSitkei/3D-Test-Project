using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialProximityCheck : MonoBehaviour
{
    float timePassed = 0;
    GameObject GameManager;
    GameManagerScript GMScript;
    bool checkForOverlap = true;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        GMScript = GameManager.GetComponent<GameManagerScript>();
    }
    void Update()
    {
        //Disable the script after 0.1 second after overlap is checked
        timePassed += 1 * Time.deltaTime;
        if (timePassed >= 0.1f)
        {
            gameObject.GetComponent<InitialProximityCheck>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            checkForOverlap = false;
        }
    }

    //Check if overlaps with other objects, reposition if so
    private void OnTriggerStay(Collider other)
    {
        if (checkForOverlap == true)
        {
            if (other.tag == "Player" || other.tag == "Collectible" || other.tag == "Blocker")
            {
                GMScript.RepositionCollectible(transform);
            }
        }
    }
}
