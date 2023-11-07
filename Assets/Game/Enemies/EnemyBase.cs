using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected int maxHP;

        public static event Action<EnemyBase> OnEnemySpawnedEvent;
        public static event Action<EnemyBase> OnEnemyDeathEvent;

        protected float currentHP;
        protected PlayerController player;
        protected Rigidbody rb;

        protected virtual void Awake()
        {
            currentHP = maxHP;
            rb = GetComponent<Rigidbody>();
        }
        protected virtual void Start()
        {            
            player = FindObjectOfType<PlayerController>();
        }
        public void GetDamage(int value)
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
    }
}
