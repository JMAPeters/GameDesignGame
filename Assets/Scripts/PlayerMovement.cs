using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    //Movement
    public Rigidbody2D rbPlayer;
    public float jumpSpeed;
    public float moveSpeed;
    public float maxSpeed;
    private float armAngle;
    bool onPlanet;
    float spaceDrag = 0.01f;
    bool playerInActive;
    GameObject Arm;
    Vector3 playerToCursor;
<<<<<<< HEAD
    public enum gunType
    {
        shotgun,
        sniper,
        AR,
        pistol
    };

    public Sprite pistolsprite;
   public static gunType guntype;
=======

>>>>>>> e21ee22d2957929a2d75180551d01a0d8be0bc99
    //Shooting
    public static GameObject player;
    public GameObject bulletPref;
    public Transform bulletSpawn;
<<<<<<< HEAD
    private float lastShot = 0.0f;
    GameObject gun;
=======
>>>>>>> e21ee22d2957929a2d75180551d01a0d8be0bc99


    static bool inMenu = false;
    //bullshit
    Sprite testPlayerSprite;
    public int bulletSpeed;

    void Start()
    {
        //testPlayerSprite = Resources.Load<Sprite>("testplayer2");
    }

    public override void OnStartLocalPlayer()
    {
        //GetComponent<SpriteRenderer>().sprite = testPlayerSprite;
    }

    void Update()
    {
        //Only local player processes input
        if (!isLocalPlayer)
            return;

        if (inMenu)
            return;

        if (playerInActive)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && onPlanet)
        {
            var jumpDirection = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * Vector3.up;
            Vector2 jumpForce = jumpDirection.normalized * jumpSpeed * 1000 * Time.deltaTime;
            rbPlayer.AddForce(jumpForce);
        }

        if (Input.GetKey(KeyCode.A))
        {
            var moveDirection = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * new Vector3(-1, -1, 0); //naar links en omlaag
            Vector2 moveForce = moveDirection.normalized * moveSpeed * 1000 * spaceDrag * Time.deltaTime;
            rbPlayer.angularVelocity = 0;

            if (rbPlayer.velocity.magnitude < maxSpeed)
            {
                rbPlayer.AddForce(moveForce);
            }
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            var moveDirection = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * new Vector3(1, -1, 0); //naar rechts en omlaag
            Vector2 moveForce = moveDirection.normalized * moveSpeed * 1000 * spaceDrag * Time.deltaTime;
            rbPlayer.angularVelocity = 0;

            if (rbPlayer.velocity.magnitude < maxSpeed)
            {
                rbPlayer.AddForce(moveForce);
            }
        }
        else
            if (onPlanet)
            rbPlayer.angularVelocity = 0;

        if (Input.GetMouseButtonDown(0))
        {
<<<<<<< HEAD
            if (Input.GetMouseButton(0) && guntype == gunType.AR ||
                Input.GetMouseButtonDown(0) && guntype == gunType.shotgun ||
                Input.GetMouseButtonDown(0) && guntype == gunType.sniper ||
                Input.GetMouseButtonDown(0) && guntype == gunType.pistol)
            {
                CmdFire();
                lastShot = Time.time;
            }
=======
            Fire(); 
>>>>>>> e21ee22d2957929a2d75180551d01a0d8be0bc99
        }
        if (GunSpecs.ammo <= 0)
        {
            guntype = gunType.pistol;
            GunSpecs.damage = 5;
            GunSpecs.fireRate = 3;
            gun = GameObject.FindGameObjectWithTag("Gun");
            gun.GetComponent<SpriteRenderer>().sprite = pistolsprite;
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onPlanet = true;
        spaceDrag = 1f;
<<<<<<< HEAD

        if (collision.gameObject.tag == "Crate")
        {
            CrateGenerator.currentCrates -= 1;
            Destroy(collision.gameObject);
            switch(Random.Range(0, 2))
            {
                case 0:
                    guntype = gunType.sniper;
                    break;
                case 1:
                    guntype = gunType.AR;
                    break;
                case 2:
                    guntype = gunType.shotgun;
                    break;
             }
           GunSpecs.SwitchWeapon();
           
        }
=======
>>>>>>> e21ee22d2957929a2d75180551d01a0d8be0bc99
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onPlanet = false;
        spaceDrag = 0.01f;
    }

    void Fire()
    {
<<<<<<< HEAD

        //Create the bullet from the bullet pref
        var bullet = Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
=======
        Quaternion bulletRotation = bulletSpawn.rotation;
        Vector2 bulletPosition = bulletSpawn.position;
        CmdFire(bulletRotation, bulletPosition);
    }
>>>>>>> e21ee22d2957929a2d75180551d01a0d8be0bc99

    [Command]
    void CmdFire(Quaternion bulletRotation, Vector2 bulletPosition)
    {
        GameObject bullet = Instantiate(bulletPref, bulletPosition, bulletRotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
        NetworkServer.Spawn(bullet);
        GunSpecs.ammo -= 1;
        //Destroy the bullet 
        Destroy(bullet, 2.0f); /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }

    public void inActive(bool value)
    {
        playerInActive = value;
    }

    public static void ToggleMenu()
    {
        inMenu = !inMenu;
    }
}