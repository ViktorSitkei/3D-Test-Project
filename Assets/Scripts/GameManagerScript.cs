using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Transform collectibleSphere, collectibleCapsule;
    public float borderPosX, borderNegX, borderPosZ, borderNegZ;
    public Transform levelFloor;
    public int currentLevel = 1, currentScore;
    public TextMesh scoreText;
    void Start()
    {
        borderPosX = levelFloor.transform.localScale.x * 5;
        borderNegX = -levelFloor.transform.localScale.x * 5;
        borderPosZ = levelFloor.transform.localScale.z * 5;
        borderNegZ = -levelFloor.transform.localScale.z * 5;
    }

    void Update()
    {

    }
    public void SpawnCollectible(GameObject collectibleType)
    {
        Vector3 spawnPos = new Vector3(Random.Range(borderNegX, borderPosX), 1, Random.Range(borderNegZ, borderPosZ));
        GameObject collectibleToSpawn = (GameObject)Instantiate(collectibleType, spawnPos,Quaternion.identity);
        collectibleToSpawn.GetComponent<Collectible>().currentLevel = currentLevel;
    }

    public void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }
}
