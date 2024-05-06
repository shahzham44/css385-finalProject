using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private float moveSpeed = 40f;
    private bool canFire = true;
    private bool isUsingRifle = true;
    private int magPosition = 1; // Just for fun

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        FaceMouse();

        FireControls();
    }

    private void PlayerInput()
    {
        // Movement
        if (Input.GetKey(KeyCode.W))
        {
            // Move up
            transform.position += Vector3.up * moveSpeed * Time.smoothDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            // Move down
            transform.position += Vector3.down * moveSpeed * Time.smoothDeltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            // Move right
            transform.position += Vector3.right * moveSpeed * Time.smoothDeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // Move left
            transform.position += Vector3.left * moveSpeed * Time.smoothDeltaTime;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            // Toggle between rifle and shotgun mode
            isUsingRifle = !isUsingRifle;
        }
    }

    private void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);

        transform.up = direction;
    }

    private void FireControls()
    {
        if (Input.GetKey(KeyCode.Space) && canFire && isUsingRifle)
        {
            StartCoroutine(FireRifle());
        }
        else if (Input.GetKey(KeyCode.Space) && canFire && !isUsingRifle)
        {
            StartCoroutine(FireShotgun());
        }
    }

    /** FireRifle()
     * ------------
     * Rifle bullets come out of the rifle in the direction the 
     * player/firearm is facing.
     * 
     * - canFire prevents spawm firing.
     * - right/upOffset used to position spawn location to player's firearm
     */
    IEnumerator FireRifle()
    {
        canFire = false;
        GameObject bullet;

        // Remove tracers 
        if (magPosition == 5)
        {
            bullet = Instantiate(Resources.Load("Prefabs/Tracer Round") as GameObject);
            magPosition = 1;
        }
        else
        {
            bullet = Instantiate(Resources.Load("Prefabs/Standard Round") as GameObject);
            magPosition++; // Just for fun
        }

        Vector3 rightoffset = transform.right * 4f;
        Vector3 upOffset = transform.up * 12f;

        bullet.transform.position = transform.position + rightoffset + upOffset;
        bullet.transform.rotation = transform.rotation;

        yield return new WaitForSeconds(0.2f);
        canFire = true;
    }

    /** FireShotgun()
     * --------------
     * Shotgun pellets come out of the (rifle), with a random count
     * ranging from 5-8 pellets at varying angles. 
     * 
     * - canFire prevents spam firing.
     * - min/maxBullets cause varying pellet counts, used in numBullets
     * - min/maxAngle cause varying angles
     * - for loop repeats however many bullets will be created
     * - right/upOffset used to position spawn location to player's firearm
     */
    IEnumerator FireShotgun()
    {
        canFire = false;

        // Define the range for the number of bullets and their angles
        int minBullets = 5;
        int maxBullets = 8;
        float minAngle = -10f;
        float maxAngle = 10f;

        // numBullets will be 5 to 8 inclusive
        int numBullets = Random.Range(minBullets, maxBullets + 1);

        // random shotgun shell number? 
        for (int i = 0; i < numBullets; i++)
        {
            GameObject bullet = Instantiate(Resources.Load("Prefabs/Shotgun Pellet") as GameObject);

            // Used in positioning bullet spawn location
            Vector3 rightoffset = transform.right * 4f;
            Vector3 upOffset = transform.up * 12f;

            // Bullet spawn location at the end of player's firearm
            bullet.transform.position = transform.position + rightoffset + upOffset;

            // Generate a random angle for the bullet
            float angle = Random.Range(minAngle, maxAngle);

            // Adjust the rotation of the bullet
            bullet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, angle));
        }

        yield return new WaitForSeconds(0.6f);
        canFire = true;
    }
}
