using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Shooter : EnemyBase
    {
        [SerializeField] private float reloadTime;
        [SerializeField] private float shootingRange;
        [SerializeField] private float force;

        [SerializeField] private Transform shootPos;

        private NavMeshAgent agent;
        private Vector3 target;
        private bool readyToFire;
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

            if (!readyToFire && PlayerIsVisible()) Reload();

            if (readyToFire && PlayerIsVisible() && PlayerIsInRange()) Shoot();
        }
        private void FixedUpdate()
        {
            if (PlayerIsInRange() && PlayerIsVisible())
            {
                agent.isStopped = true;
                return;
            }
            agent.isStopped = false;
            Move();
        }
        private void Move()
        {
            agent.SetDestination(target);
        }
        private void Shoot()
        {
            var proj = PoolManager.GetEnemyProjectile();
            Physics.IgnoreCollision(coll, proj.GetComponent<Collider>());
            proj.gameObject.SetActive(true);
            proj.transform.position = shootPos.position;
            proj.Initialize(target, force, damage);

            readyToFire = false;
        }
        private void Reload()
        {
            if (timer >= reloadTime)
            {
                timer -= reloadTime;
                readyToFire = true;
            }
            else
                timer += Time.deltaTime;
        }
        private bool PlayerIsInRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) <= shootingRange;
        }
    }
}
