using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string type;
    public int scoreValue;
    public int currentLevel;

    void Start()
    {
        if (type == "Sphere")
        {
            //Exception made for LVL1 Sphere as specified score does not fit formula used for other cases
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
            scoreValue = ((currentLevel - 1) * 10) + 2;      
        }
    }
}
