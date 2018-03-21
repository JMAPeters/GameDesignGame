using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpecs : MonoBehaviour {

    public static int damage;
    public static int ammo;
    public static float fireRate;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void SwitchWeapon()
    {
        if (PlayerMovement.guntype == PlayerMovement.gunType.AR)
        {
            damage = 30;
            ammo = 60;
            fireRate = 10f;

        }
        if (PlayerMovement.guntype == PlayerMovement.gunType.shotgun)
        {
            damage = 40;
            ammo = 20;
            fireRate = 1f;
        }
        if (PlayerMovement.guntype == PlayerMovement.gunType.sniper)
        {
            damage = 50;
            ammo = 20;
            fireRate = 0.5f;
        }
    }
}
