using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.smoothDeltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.smoothDeltaTime;
        }
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            KeyBehavior key = collision.gameObject.GetComponent<KeyBehavior>();
            key.DestroyDoor();
        }
    }
}
