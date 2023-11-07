using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected int maxHP;
        [SerializeField] protected float damage = 1;
        [SerializeField] private LayerMask wallsLayer;

        public static event Action<EnemyBase> OnEnemySpawnedEvent;
        public static event Action<EnemyBase> OnEnemyDeathEvent;

        protected float currentHP;
        protected PlayerController player;
        protected Rigidbody rb;
        protected Collider coll;

        protected const int wallsLayerNumber = 6;

        protected virtual void Awake()
        {
            currentHP = maxHP;
            rb = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();
        }
        protected virtual void Start()
        {            
            player = FindObjectOfType<PlayerController>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GetDamage(damage);
            }
        }
        public void GetDamage(float value)
        {
            currentHP -= value;

            if (currentHP <= 0)
            {
                KillEnemy();
            }
        }
        protected void KillEnemy()
        {
            // death effect
            Deactivate();
        }
        protected void Deactivate()
        {
            currentHP = maxHP;
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
        protected void OnEnemySpawned(EnemyBase enemy)
        {
            OnEnemySpawnedEvent?.Invoke(enemy);
        }
        protected void OnEnemyDeath(EnemyBase enemy)
        {
            OnEnemyDeathEvent?.Invoke(enemy);
        }
        protected bool PlayerIsVisible()
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, player.transform.position, out hit, wallsLayer))
            {
                if (hit.collider.gameObject.layer == wallsLayerNumber)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
