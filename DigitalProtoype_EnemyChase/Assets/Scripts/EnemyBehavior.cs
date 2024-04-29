using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyBehavior : MonoBehaviour
{
    private float speed = 30.0f;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = player.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.smoothDeltaTime);
    }
}
