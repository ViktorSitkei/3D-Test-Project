using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SaveData 
{
    public float time;
    public int objectsCollected;
    public int score;

    public SaveData(float time, int objectsCollected, int score)
    {
        this.time = time;
        this.objectsCollected = objectsCollected;
        this.score = score;
    }
}
