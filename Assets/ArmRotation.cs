using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    GameObject player;
    float prevRotZ;
    void Start()
    {
        player = GameObject.Find("Player");
    }
	// Update is called once per frame
	void Update () {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        Debug.Log(rotZ);

        if (player == null)
            player = GameObject.Find("Player");
        if (prevRotZ < 90 && prevRotZ > -90 && ((rotZ > 90 && rotZ < 180) || (rotZ > -180 && rotZ < -90)))
            player.GetComponent<SpriteRenderer>().flipY = true;
        else if (rotZ < 90 && rotZ > -90 && ((prevRotZ > 90 && prevRotZ < 180) || (prevRotZ > -180 && prevRotZ < -90)))
            player.GetComponent<SpriteRenderer>().flipY = false;
        prevRotZ = rotZ; 


        
    }
}
