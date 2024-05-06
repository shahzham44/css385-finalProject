using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBulletBehavior : MonoBehaviour
{
    private const float bulletSpeed = 400f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (bulletSpeed * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Tag collision stuff
    }

    private void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }
}
