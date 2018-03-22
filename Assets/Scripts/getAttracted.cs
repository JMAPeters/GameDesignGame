using UnityEngine;

public class getAttracted : MonoBehaviour
{
    public Rigidbody2D rbPlayer;
    public int gravitation;
    public int maxAttractionDistance;

    bool onPlanet = false;

    GameObject[] Planets;
    GameObject nearestPlanet;


    void Start()
    {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
    }

    void FixedUpdate()
    {
        nearestPlanet = NearestPlanet();

        if (onPlanet)
            attractTo(nearestPlanet);
        else
            foreach (GameObject Planet in Planets)
                attractTo(Planet);

        rotatePlayer(nearestPlanet);
    }

    GameObject NearestPlanet()
    {
        float nearestPlanetDistance = maxAttractionDistance;
        foreach (GameObject Planet in Planets)
        {
            Rigidbody2D rbPlanet = Planet.GetComponent<Rigidbody2D>();

            Vector3 direction = rbPlanet.position - rbPlayer.position;
            float distance = direction.magnitude;

            if (distance < nearestPlanetDistance)
            {
                nearestPlanetDistance = distance;
                nearestPlanet = Planet;
            }
        }
        return nearestPlanet;
    }

    void attractTo(GameObject Planet)
    {
        Rigidbody2D rbPlanet = Planet.GetComponent<Rigidbody2D>();

        Vector3 direction = rbPlanet.position - rbPlayer.position;
        float distance = direction.magnitude;

        if (distance < maxAttractionDistance)
        {
            float forceMagnitude = gravitation * (rbPlanet.mass * rbPlayer.mass) / Mathf.Pow(distance, 2) * Time.deltaTime;
            Vector2 force = direction.normalized * forceMagnitude;

            rbPlayer.AddForce(force);
        }
    }

    void rotatePlayer(GameObject Planet)
    {
        Rigidbody2D rbPlanet = Planet.GetComponent<Rigidbody2D>();

        Vector3 direction = rbPlanet.position - rbPlayer.position;

        //Change angle of the sprite
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion Quat = Quaternion.AngleAxis(angle, Vector3.forward);

        rbPlayer.transform.rotation = Quaternion.Slerp(transform.rotation, Quat, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
            onPlanet = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
            onPlanet = false;
    }
}