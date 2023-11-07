using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Chaser : EnemyBase
    {
        private NavMeshAgent agent;

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
        }
        private void FixedUpdate()
        {
            Move();
        }
        private void Move()
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
