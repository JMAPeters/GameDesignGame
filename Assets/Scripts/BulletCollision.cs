using UnityEngine;

namespace Prototype.NetworkLobby
{
    public class BulletCollision : MonoBehaviour
    {

        int bulletDamage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                bulletDamage = GunSpecs.damage;
                var collisionObject = collision.gameObject;
                var health = collisionObject.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.TakeDamage(bulletDamage);
                }
            }
            Destroy(gameObject);
        }
    }
}
