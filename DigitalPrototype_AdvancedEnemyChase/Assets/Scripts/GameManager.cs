using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool playerFound;

    // Start is called before the first frame update
    void Start()
    {
        playerFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlayerFound(bool status)
    {
        playerFound = status;
    }

    public bool isPlayerFound()
    {
        return playerFound;
    }
}
