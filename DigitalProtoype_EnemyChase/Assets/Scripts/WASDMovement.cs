using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    private float speed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            pos.y += speed * Time.smoothDeltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.smoothDeltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= speed * Time.smoothDeltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.smoothDeltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2.0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2.0f;
        }

        transform.position = pos;
    }
}
