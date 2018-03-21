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
    bool onPlanet;
    float spaceDrag = 0.01f;
    bool playerInActive;

    //Shooting
    public static GameObject player;
    public GameObject bulletPref;
    public Transform bulletSpawn;


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
            CmdFire(); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onPlanet = true;
        spaceDrag = 1f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onPlanet = false;
        spaceDrag = 0.01f;
    }

    [Command]
    void CmdFire()
    {
        //Create the bullet from the bullet pref
        var bullet = Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation) as GameObject;

        //Add veloctiy to the bullet
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