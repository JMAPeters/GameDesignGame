using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    bool onPlanet;
    public Rigidbody2D rbCrate;

    void Update()
    {
        if (onPlanet)
        {
            rbCrate.velocity = Vector2.zero;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
            onPlanet = true;
    }
}
