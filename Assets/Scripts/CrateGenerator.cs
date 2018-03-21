using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateGenerator : MonoBehaviour
{

    public List<GameObject> Crates;
    public int maxCrateCount, minCrateCount, padding;
    public GameObject Crate;
    private int xPos, yPos, random;
    private float spawnDelay, spawnTimer;
    public GameObject planet1, planet2;

	// Use this for initialization
	void Start ()
    {
        Crates = new List<GameObject>();
        spawnDelay = 4.0f;
        spawnTimer = spawnDelay;
        addCrate();
    }


    public void addCrate()
    {
        Rigidbody2D rbCrate = Crate.GetComponent<Rigidbody2D>();
        Crate.transform.position = RandomPosition();
        Crates.Add(Crate);
        Instantiate(Crate);
    }

    private Vector2 RandomPosition()
    {
        random = Random.Range(0, 3);
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
                yPos = Random.Range((int)planet2.transform.position.y, (int)planet1.transform.position.y);
                break;
            case 3:
                xPos = Random.Range((int)planet1.transform.position.x, (int)planet2.transform.position.x);
                yPos = (int)planet2.transform.position.y + padding;
            break;
        }

        Vector2 Position = new Vector2(xPos, yPos);
        return Position;
    }

	// Update is called once per frame
	void Update ()
    {
		if(Crates.Count < minCrateCount || (spawnTimer <= 0 && Crates.Count < maxCrateCount))
        {
            addCrate();        
            spawnTimer = spawnDelay;
            /*foreach (GameObject c in Crates)
            {
<<<<<<< Updated upstream
                if (c.transform.position.x < planet1.transform.position.x || c.transform.position.x > planet2.transform.position.x ||
                    c.transform.position.y < planet2.transform.position.y || c.transform.position.y > planet1.transform.position.y)
                {
                    Crates.Remove(c);
                    Debug.Log("Hopelijk zijn dit er niet veel");
                }
            }*/
               // if (c.transform.position.x < )
           }       
        spawnTimer -= 0.01f;
	}
}
