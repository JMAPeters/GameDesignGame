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

    public enum gunType
    {
        shotgun,
        sniper,
        AR,
        pistol
    };

    public Sprite pistolsprite;
    public static gunType guntype;
    //Shooting
    public static GameObject player;
    public GameObject bulletPref;
    public GameObject armPref;
    public Transform bulletSpawn;
    private float lastShot = 0.0f;
    GameObject gun;
    public Quaternion shotgunAngle;


    static bool inMenu = false;
    //bullshit
    Sprite testPlayerSprite;
    public int bulletSpeed;

    private AudioSource source;
    public AudioClip arSound;
    public AudioClip shotgunSound;
    public AudioClip sniperSound;

    void Start()
    {
        source = armPref.GetComponent<AudioSource>();
    }

    void FixedUpdate()
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
            transform.GetComponent<SpriteRenderer>().flipX = true;

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
            transform.GetComponent<SpriteRenderer>().flipX = false;

            if (rbPlayer.velocity.magnitude < maxSpeed)
            {
                rbPlayer.AddForce(moveForce);
            }
        }
        else
        if (onPlanet)
            rbPlayer.angularVelocity = 0;

        
        if (Time.time > 1 / GunSpecs.fireRate + lastShot)
        {
            if (Input.GetMouseButton(0) && guntype == gunType.AR ||
            Input.GetMouseButtonDown(0) && guntype == gunType.shotgun ||
            Input.GetMouseButtonDown(0) && guntype == gunType.sniper ||
            Input.GetMouseButtonDown(0) && guntype == gunType.pistol)
            {
                Fire();
                lastShot = Time.time;

            }
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
        if (collision.gameObject.tag == "Planet")
        {
            onPlanet = true;
            spaceDrag = 1f;
        }

        if (collision.gameObject.tag == "Crate")
        {
            CrateGenerator.currentCrates -= 1;
            Destroy(collision.gameObject);

            if (!isLocalPlayer)
                return;

            switch (Random.Range(0, 3))
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            onPlanet = false;
            spaceDrag = 0.01f;
        }
    }

    void Fire()
    {
        //Create the bullet from the bullet pref
        Quaternion bulletRotation = bulletSpawn.rotation;
        Vector2 bulletPosition = bulletSpawn.position;
        CmdFire(bulletRotation, bulletPosition);
        if (GunSpecs.ammo >= 1)
            GunSpecs.ammo -= 1;
    }

    [Command]
    void CmdFire(Quaternion bulletRotation, Vector2 bulletPosition)
    {
        if (guntype == gunType.pistol)
        {
            source.clip = arSound;
            source.PlayOneShot(arSound);
        }
        if (guntype == gunType.AR)
        {
            source.clip = arSound;
            source.PlayOneShot(arSound);
        }
        if (guntype == gunType.sniper)
        {
            source.clip = sniperSound;
            source.PlayOneShot(sniperSound);
        }
        if (guntype == gunType.shotgun)
        {
            source.clip = shotgunSound;
            source.PlayOneShot(shotgunSound);
        }

        GameObject bullet = Instantiate(bulletPref, bulletPosition, bulletRotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
        NetworkServer.Spawn(bullet);


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

/*        if (guntype == gunType.shotgun)
        {
            GameObject bullet2 = Instantiate(bulletPref, bulletPosition, bulletRotation);
            bullet2.GetComponent<Rigidbody2D>().velocity = bullet2.transform.right * bulletSpeed;
            NetworkServer.Spawn(bullet2);
            if (GunSpecs.ammo >= 1)
                GunSpecs.ammo -= 1;

            GameObject bullet3 = Instantiate(bulletPref, bulletPosition, bulletRotation);
            bullet3.GetComponent<Rigidbody2D>().velocity = bullet3.transform.right * bulletSpeed;
            NetworkServer.Spawn(bullet3);
            if (GunSpecs.ammo >= 1)
                GunSpecs.ammo -= 1;
        }*/
