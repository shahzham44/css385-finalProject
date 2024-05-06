

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehavior : MonoBehaviour
{
    private float speed = 10.0f;

    private float radius = 20.0f;

    private float angle = 45.0f;

    private GameObject player;

    private GameManager gameManager;

    private bool chasePlayer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chasePlayer = false;
        gameManager = FindAnyObjectByType<GameManager>();
        StartCoroutine(isSomethingInVision());

    }

    //Referenced using: https://www.youtube.com/watch?v=OQ1dRX5NyM0
    private IEnumerator isSomethingInVision()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return waitTime;
            SomethingInVision();
        }
    }

    private void SomethingInVision()
    {
        if (!gameManager.isPlayerFound())
        {
            Collider2D[] overallRange = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Player"));
            if (overallRange.Length > 0)
            {
                Transform currentTarget = overallRange[0].transform;
                Vector2 direction = (currentTarget.position - transform.position).normalized;
                if (Vector2.Angle(transform.up, direction) < angle / 2)
                {
                    float distance = Vector2.Distance(transform.position, currentTarget.position);
                    if (!Physics2D.Raycast(transform.position, direction, distance, LayerMask.GetMask("Wall")))
                    {
                        gameManager.setPlayerFound(true);
                    }
                }
            }
        }
    }

    // Referenced: https://www.youtube.com/watch?v=xDg2pxqJHq4
    // Update is called once per frame
    void Update()
    { 
        if (gameManager.isPlayerFound())
        {
            Debug.Log("Chasing Player");
            RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 40.0f);
            if (lineOfSight.collider != null)
            {
                if (lineOfSight.collider.CompareTag("Player"))
                {
                    chasePlayer = true;
                }
            }
        }

        if (chasePlayer)
        {
            transform.up = player.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.smoothDeltaTime);
        }
    }

    /*
    private void FixedUpdate()
    {
        RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 15.0f);

        if (lineOfSight.collider != null)
        {
            Debug.Log(lineOfSight.collider.name);
            detectPlayer = lineOfSight.collider.CompareTag("Player");
            if (detectPlayer)
            {
                Debug.Log("Detects Player");
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.yellow);
                if (Vector3.Distance(player.transform.position, transform.position) > 15.0f)
                {
                    detectPlayer = false;
                    Debug.Log("Player has went beyond the range");
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                }
            }
        }
    }
    */
}
