using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialProximityCheck : MonoBehaviour
{
    float timePassed = 0;
    GameManagerScript GMScript;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += 1 * Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Collectible" || other.tag == "Blocker")
        {
            // GM.SpawnCollectible();
        }
    }
}
