using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Chaser : EnemyBase
    {
        [SerializeField] private float reloadTime;

        private NavMeshAgent agent;
        private Vector3 target;
        private bool readyToAttack;
        private float timer = 0f;

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

            if (!readyToAttack && PlayerIsVisible()) Reload();
        }
        private void FixedUpdate()
        {
            if (!readyToAttack)
            {
                agent.isStopped = true;
                return;
            }
            agent.isStopped = false;
            Move();
        }
        private void OnCollisionEnter(Collision collision)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.GetDamage(damage);
                readyToAttack = false;
            }
        }
        private void Move()
        {
            agent.SetDestination(target);
        }
        private void Reload()
        {
            if (timer >= reloadTime)
            {
                timer -= reloadTime;
                readyToAttack = true;
            }
            else
                timer += Time.deltaTime;
        }
    }
}
