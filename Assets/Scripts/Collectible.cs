using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string type;
    public int scoreValue;
    GameManagerScript GMScript;
    public int currentLevel;
    void Start()
    {
        //GMScript = GameManagerScript;
        //int currentLevel = GMScript.currentLevel;
        if (type == "Sphere")
        {
            if (currentLevel == 1)
            {
                scoreValue = 1;
            }
            else
            {
                scoreValue = (currentLevel -1) * 10;
            }
        }
        if (type == "Capsule")
        {
            scoreValue = ((currentLevel - 1) * 10) + 2
;       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
