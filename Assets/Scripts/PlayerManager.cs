using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Vector2 currentVelocity;
    public float moveSpeed;
    GameObject GameManager;
    GameManagerScript GMScript;
    string lastCollectible;
    bool isMovingX,isMovingY;
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        GMScript = GameManager.GetComponent<GameManagerScript>();
    }

    void Update()
    {
        //Setting player Velocity to control movement
        if (!GMScript.playerWon)
        {
            //reset velocity at the start of update
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            isMovingX = false;
            isMovingY = false;

            //Check for input, set velocity depending on input
            if (Input.GetKey(KeyCode.W))
            {
                currentVelocity = new Vector2(currentVelocity.x, moveSpeed);
                if (Input.GetKey(KeyCode.S))
                {
                    currentVelocity = new Vector2(currentVelocity.x, 0);
                }
                isMovingY = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                currentVelocity = new Vector2(currentVelocity.x, -moveSpeed);
                if (Input.GetKey(KeyCode.W))
                {
                    currentVelocity = new Vector2(currentVelocity.x, 0);
                }
                isMovingY = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                currentVelocity = new Vector2(moveSpeed, currentVelocity.y);
                if (Input.GetKey(KeyCode.A))
                {
                    currentVelocity = new Vector2(0, currentVelocity.y);
                }
                isMovingX = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                currentVelocity = new Vector2(-moveSpeed, currentVelocity.y);
                if (Input.GetKey(KeyCode.D))
                {
                    currentVelocity = new Vector2(0, currentVelocity.y);
                }
                isMovingX = true;
            }

            //Correction for simultanious control key presses
            if (isMovingX && !isMovingY)
            {
                transform.GetComponent<Rigidbody>().velocity = new Vector3(currentVelocity.x, 0, 0);
            }
            else if (!isMovingX && isMovingY)
            {
                transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, currentVelocity.y);
            }
            else if (isMovingX && isMovingY)
            {
                transform.GetComponent<Rigidbody>().velocity = new Vector3(currentVelocity.x, 0, currentVelocity.y);
            }
            //Reset Velocity if no key is pressed
            else
            {
                currentVelocity = Vector2.zero;
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        //if player has won stop from moving
        else
        {
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    // Upon touching collectible, check type of collectible, spawn new random collectible, add to score, destroy touched collectible
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible")
        {
            if (!other.GetComponent<InitialProximityCheck>().isActiveAndEnabled)
            {
                if (other.GetComponent<Collectible>().type == "Sphere")
                {
                    GMScript.sphereCollectibles -= 1;
                }
                else if (other.GetComponent<Collectible>().type == "Capsule")
                {
                    GMScript.capsuleCollectibles -= 1;
                }

                GMScript.SpawnRandomCollectible();

                GMScript.collectedCollectibles += 1;

                if (lastCollectible == other.GetComponent<Collectible>().type)
                    DeductFromScore(other.transform);
                else
                    AddToScore(other.transform);

                lastCollectible = other.GetComponent<Collectible>().type;
                GMScript.UpdateStatsText();
                GameObject.Destroy(other.gameObject);
            }
        }
    }
    //Add to score & check if player has won or hit a new level
    void AddToScore(Transform collectible)
    {
        GMScript.currentScore += collectible.GetComponent<Collectible>().scoreValue;
        if(GMScript.currentScore >= 400)
        {
            GMScript.SaveData();
            GMScript.playerWon = true;
            GMScript.UpdateStatsText();
        }
        if (GMScript.currentScore >= GMScript.currentLevel * 100)
        {
            if(GMScript.currentScore < 300)
            {
                GMScript.InitiateNextLevel();
            }
        }
    }

    //Deduct from score if same type hit twice
    void DeductFromScore(Transform collectible)
    {
        GMScript.currentScore -= collectible.GetComponent<Collectible>().scoreValue * 2;
    }
}
