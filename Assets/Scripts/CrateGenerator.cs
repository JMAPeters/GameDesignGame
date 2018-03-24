using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CrateGenerator : NetworkBehaviour
{
    public GameObject Crate;
    public int maxCrateCount, padding;
    
    private int xPos, yPos, random;
    private float spawnTimer;
    public float spawnDelay;
    public GameObject planet1, planet2;

    public static int currentCrates = 0;

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            if (currentCrates < maxCrateCount)
            CreateCrate();

            spawnTimer = spawnDelay;
        }
    }

    void CreateCrate()
    {
        GameObject crate = Instantiate(Crate);
        Crate.transform.position = RandomPosition();
        NetworkServer.Spawn(crate);
        currentCrates += 1;
    }

    private Vector2 RandomPosition()
    {
        random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                xPos = (int)planet2.transform.position.x + padding;
                yPos = Random.Range((int)planet1.transform.position.y, (int)planet2.transform.position.y);
                break;
            case 1:
                xPos = Random.Range((int)planet1.transform.position.x, (int)planet2.transform.position.x);
                yPos = (int)planet1.transform.position.y - padding;
                break;
            case 2:
                xPos = (int)planet1.transform.position.x - padding;
                yPos = Random.Range((int)planet1.transform.position.y, (int)planet2.transform.position.y);
                break;
            case 3:
                xPos = Random.Range((int)planet1.transform.position.x, (int)planet2.transform.position.x);
                yPos = (int)planet2.transform.position.y + padding;
                break;
        }

        Vector2 Position = new Vector2(xPos, yPos);
        return Position;
    }
}
