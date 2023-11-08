using Enemies;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
public class ShotgunProjectile : MonoBehaviour
{
    private Rigidbody rb;
    private float damage;

    private const float deviation = 0.7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.GetDamage(damage);
        }

        Deactivate();
    }

    public void Initialize(Vector3 target, float force, float damage)
    {
        this.damage = damage;

        Vector3 newTarget = new Vector3
            (target.x + Random.Range(-deviation, deviation),
            target.y,
            target.z + Random.Range(-deviation, deviation));
        rb.AddForce((newTarget - transform.position).normalized * force * 25);
    }
    private void Deactivate()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
