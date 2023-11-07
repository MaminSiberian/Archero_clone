using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
    public class EnemyProjectile : MonoBehaviour
    {
        private Rigidbody rb;
        private float damage;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GetDamage(damage);
            }
            Deactivate();
        }

        public void Initialize(Vector3 target, float force, float damage)
        {
            this.damage = damage;

            rb.AddForce((target - transform.position).normalized * force);
        }
        private void Deactivate()
        {
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
