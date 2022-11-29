using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Vector2 pos;
    public float moveSpeed;
    GameObject GameManager;
    GameManagerScript GMScript;
    public string lastCollectible;
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        GMScript = GameManager.GetComponent<GameManagerScript>();
        pos = new Vector2(transform.position.x, transform.position.z);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            pos += new Vector2(0, moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos += new Vector2(0, -moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos += new Vector2(moveSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos += new Vector2(-moveSpeed * Time.deltaTime, 0);
        }
        transform.position = new Vector3(pos.x, 1, pos.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible")
        {
            Debug.Log("ote triggered");
            if (other.GetComponent<Collectible>().type == "Capsule")
                GMScript.SpawnCollectible(other.gameObject);

            if (other.GetComponent<Collectible>().type == "Sphere")
                GMScript.SpawnCollectible(other.gameObject);



            if (lastCollectible == other.GetComponent<Collectible>().type)
                DeductFromScore(other.transform);
            else
                AddToScore(other.transform);

            lastCollectible = other.GetComponent<Collectible>().type;
            GMScript.UpdateScoreText();
            GameObject.Destroy(other.gameObject);
        }
    }

    void AddToScore(Transform collectible)
    {
        GMScript.currentScore += collectible.GetComponent<Collectible>().scoreValue;
    }
    void DeductFromScore(Transform collectible)
    {
        GMScript.currentScore -= collectible.GetComponent<Collectible>().scoreValue * 2;

    }
}
