using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Bomber : EnemyBase
    {
        private NavMeshAgent agent;
        private Vector3 target;

        protected void OnEnable()
        {
            OnEnemySpawned(this);
        }
        protected void OnDisable()
        {
            OnEnemyDeath(this);
        }
        protected override void Awake()
        {
            base.Awake();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;
        }
        protected override void Start()
        {
            base.Start();
            OnEnemySpawned(this);
            target = transform.position;
        }
        private void Update()
        {
            if (PlayerIsVisible())
            {
                target = player.transform.position;
                transform.LookAt(target);
            }
        }
        private void FixedUpdate()
        {
            Move();
        }
        private void OnCollisionEnter(Collision collision)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                KillEnemy();
            }
        }
        private void Move()
        {
            agent.SetDestination(target);
        }
        public override void GetDamage(float value)
        {
            currentHP -= value;

            if (currentHP <= 0)
            {
                KillEnemy();
            }
        }
        protected override void KillEnemy()
        {
            var exp = PoolManager.GetExplosion();
            exp.transform.position = transform.position;
            exp.gameObject.SetActive(true);

            base.KillEnemy();
        }
    }
}
