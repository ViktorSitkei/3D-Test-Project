using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Vector2 pos;
    public float moveSpeed;
    void Start()
    {
        pos = new Vector2(transform.position.x,transform.position.z);
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
        transform.position = new Vector3(pos.x,1,pos.y);
    }
    
}
