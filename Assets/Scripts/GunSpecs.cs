using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpecs : MonoBehaviour {

    public static int damage;
    public static int ammo = 0;
    public static float fireRate;
    public Sprite ArSprite, sniperSprite, shotgunSprite;
    static Sprite ArSpriteStatic, sniperSpriteStatic, shotgunSpriteStatic;
    static GameObject gun;

	void Start () {
        gun = GameObject.FindGameObjectWithTag("Gun");
        ArSpriteStatic = ArSprite;
        sniperSpriteStatic = sniperSprite;
        shotgunSpriteStatic = shotgunSprite;
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
            gun.GetComponent<SpriteRenderer>().sprite = ArSpriteStatic;

        }
        if (PlayerMovement.guntype == PlayerMovement.gunType.shotgun)
        {
            damage = 40;
            ammo = 10;
            fireRate = 1f;
            gun.GetComponent<SpriteRenderer>().sprite = shotgunSpriteStatic;

        }
        if (PlayerMovement.guntype == PlayerMovement.gunType.sniper)
        {
            damage = 50;
            ammo = 10;
            fireRate = 0.5f;
            gun.GetComponent<SpriteRenderer>().sprite = sniperSpriteStatic;
        }
       
    }
}
