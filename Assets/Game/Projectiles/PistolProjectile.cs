using Enemies;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
public class PistolProjectile : MonoBehaviour
{
    private Rigidbody rb;
    private Transform target;
    private float speed;
    private float damage;

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
    private void FixedUpdate()
    {
        Move();
    }

    public void Initialize(Transform target, float speed, float damage)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;
    }
    private void Move()
    {
        rb.MovePosition(transform.position + (target.position - transform.position).normalized * speed * Time.fixedDeltaTime);
    }
    private void Deactivate()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
