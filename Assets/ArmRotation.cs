using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArmRotation : NetworkBehaviour {

    public GameObject player;
    public SpriteRenderer spriteRenderer;
    static SpriteRenderer staticSpriteRenderer;

    public Sprite spriteArmScout, spriteArmSoldier, spriteArmTank;
    static Sprite staticSpriteArmScout, staticSpriteArmSoldier, staticSpriteArmTank;
    float prevRotZ;

    void Start()
    {
        staticSpriteRenderer = spriteRenderer;
        staticSpriteArmScout = spriteArmScout;
        staticSpriteArmSoldier = spriteArmSoldier;
        staticSpriteArmTank = spriteArmTank;
    }

	// Update is called once per frame
	void Update () {
        if (!player.GetComponent<NetworkIdentity>().isLocalPlayer)
            return;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
       /* Debug.Log(rotZ);

        if (player == null)
            player = GameObject.Find("Player");
        if (prevRotZ < 90 && prevRotZ > -90 && ((rotZ > 90 && rotZ < 180) || (rotZ > -180 && rotZ < -90)))
            player.GetComponent<SpriteRenderer>().flipY = true;
        else if (rotZ < 90 && rotZ > -90 && ((prevRotZ > 90 && prevRotZ < 180) || (prevRotZ > -180 && prevRotZ < -90)))
            player.GetComponent<SpriteRenderer>().flipY = false;
        prevRotZ = rotZ; */
    }

    public static void SetSprite(string character)
    {
        if (character == "Scout")
        {
            staticSpriteRenderer.sprite = staticSpriteArmScout;
        }
        if (character == "Soldier")
        {
            staticSpriteRenderer.sprite = staticSpriteArmSoldier;
        }
        if (character == "Tank")
        {
            staticSpriteRenderer.sprite = staticSpriteArmTank;
        }
    }
}
