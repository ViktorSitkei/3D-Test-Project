using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private float borderPosX, borderNegX, borderPosZ, borderNegZ;
    private float secondTimer, blockerSpawnTimer;

    [HideInInspector]
    public float timePassed;
    [HideInInspector]
    public int sphereCollectibles, capsuleCollectibles;
    [HideInInspector]
    public bool playerWon;
    [HideInInspector]
    public int currentLevel, currentScore, collectedCollectibles;


    public Transform collectibleSphere, collectibleCapsule, blockerCube;
    public Transform levelFloor;
    public TextMesh statsText, youWonText;
    public GameObject collectiblesParent, blockerParent;
    public GameObject player;
    public SaveDataManager dataManager;
    public SaveData saveData;
    public float blockerInterval;


    void Start()
    {
        //Setting the level where collectibles and blockers can spawn depending on the "floor"object's dimensions, could be hardcoded if the area would always be constant.
        borderPosX = levelFloor.transform.localScale.x * 5;
        borderNegX = -levelFloor.transform.localScale.x * 5;
        borderPosZ = levelFloor.transform.localScale.z * 5;
        borderNegZ = -levelFloor.transform.localScale.z * 5;
        InitiateNextLevel();
    }

    void Update()
    {
        //Restart Game with R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
      
        if (!playerWon)
        {
            timePassed += Time.deltaTime;

            //secondTimer helps to updates the scoreboard every second to save cycles.
            secondTimer += Time.deltaTime;
            if (secondTimer >= 1)
            {
                UpdateStatsText();
                secondTimer = 0;
            }

            blockerSpawnTimer += Time.deltaTime;
            if (blockerSpawnTimer >= blockerInterval)
            {
                SpawnBlocker();
                blockerSpawnTimer = 0;
            }
        }
        else
        {
            youWonText.text = "You Won!";
        }
    }

    //Spawning new Collectible
    public void SpawnRandomCollectible()
    {
        //Checking currently available collectibles, if a single type is too prominant, force algorithm to spawn other type
        if (sphereCollectibles >= 8)
        {
            Vector3 spawnPos = new Vector3(Random.Range(borderNegX, borderPosX), 1, Random.Range(borderNegZ, borderPosZ));
            Transform collectibleToSpawn = (Transform)Instantiate(collectibleCapsule, spawnPos, Quaternion.identity);
            collectibleToSpawn.parent = collectiblesParent.transform;
            collectibleToSpawn.GetComponent<Collectible>().currentLevel = currentLevel;
            capsuleCollectibles += 1;

        }
        else if (capsuleCollectibles >= 8)
        {
            Vector3 spawnPos = new Vector3(Random.Range(borderNegX, borderPosX), 1, Random.Range(borderNegZ, borderPosZ));
            Transform collectibleToSpawn = (Transform)Instantiate(collectibleSphere, spawnPos, Quaternion.identity);
            collectibleToSpawn.parent = collectiblesParent.transform;
            collectibleToSpawn.GetComponent<Collectible>().currentLevel = currentLevel;
            sphereCollectibles += 1;

        }

        //In Balanced Collectible scenario, spawn Random type of Collectible at random position
        else
        {
            int randomInt = Random.Range(0, 2);
            Transform randomCollectible;
            Vector3 spawnPos = new Vector3(Random.Range(borderNegX, borderPosX), 1, Random.Range(borderNegZ, borderPosZ));

            if (randomInt == 0)
            {
                randomCollectible = collectibleSphere;
                sphereCollectibles += 1;
            }
            else if (randomInt == 1)
            {
                randomCollectible = collectibleCapsule;
                capsuleCollectibles += 1;
            }
            else
            {
                randomCollectible = null;
            }

            Transform collectibleToSpawn = (Transform)Instantiate(randomCollectible, spawnPos, Quaternion.identity);
            collectibleToSpawn.parent = collectiblesParent.transform;
            collectibleToSpawn.GetComponent<Collectible>().currentLevel = currentLevel;
        }

    }

    //Repositioning objects that may overlap with player upon spawning to prevent accidental collection 
    public void RepositionCollectible(Transform collectible)
    {
        Vector3 spawnPos = new Vector3(Random.Range(borderNegX, borderPosX), 1, Random.Range(borderNegZ, borderPosZ));
        collectible.transform.position = spawnPos;
    }

    //Initiate Level with Collecibles and wiping previous objects
    public void InitiateNextLevel()
    {
        //Destroying existing Collectibles on current level
        for (var i = 0; i < collectiblesParent.transform.childCount; i++)
        {
            Destroy(collectiblesParent.transform.GetChild(i).gameObject);
        }
        sphereCollectibles = 0;
        capsuleCollectibles = 0;
        currentLevel += 1;

        //Destroying existing Blockers on current level

        for (var i = 0; i < blockerParent.transform.childCount; i++)
        {
            Destroy(blockerParent.transform.GetChild(i).gameObject);
        }

        //Spawn 10 Random Collectible in Random locations
        for (int i = 0; i < 10; i++)
        {
            SpawnRandomCollectible();        
        }
        //Reposition Player to centre for next Level & scale Player depending on game Level
        player.transform.localScale = new Vector3(currentLevel, 1, currentLevel);
        player.transform.position = new Vector3(0, 1, 0);
    }

    //Spawn Cubes to block player from moving
    public void SpawnBlocker()
    {
        Vector3 spawnPos = new Vector3(Random.Range(borderNegX, borderPosX), 1, Random.Range(borderNegZ, borderPosZ));
        Transform blockerToSpawn = (Transform)Instantiate(blockerCube, spawnPos, Quaternion.identity);
        blockerToSpawn.parent = blockerParent.transform;
    }

    //Updating HUD with Score, Collected Collectibles, Level, Time passed.
    public void UpdateStatsText()
    {
        statsText.text = "Score: " + currentScore.ToString()  + "         Collected: " + collectedCollectibles.ToString() + "\n" + "Level " + currentLevel.ToString() + "         Timer: " + timePassed.ToString("F0");
    }

    //Update SaveData and write save file to JSON at C:\Users\"Username"\AppData\LocalLow\DefaultCompany\3D Test Project
    public void SaveData()
    {
        dataManager.CreatePlayerData();
        dataManager.Save();
    }
}
