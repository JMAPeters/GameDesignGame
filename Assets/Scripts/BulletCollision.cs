using UnityEngine;

namespace Prototype.NetworkLobby
{
    public class BulletCollision : MonoBehaviour
    {
        static GameObject player;

        public int bulletDamage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                var collisionObject = collision.gameObject;
                var health = collisionObject.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.TakeDamage(bulletDamage);
                }
            }
            Destroy(gameObject);
        }

        public static void SetPlayer(GameObject _player)
        {
            player = _player;
        }
    }
}
