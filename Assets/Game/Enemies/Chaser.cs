using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Chaser : EnemyBase
    {
        [SerializeField] private float reloadTime;
        [SerializeField] private float shootingRange;
        [SerializeField] private float force;

        [SerializeField] private Transform shootPos;
        [SerializeField] private LayerMask wallsLayer;

        private NavMeshAgent agent;
        private Vector3 target;
        private bool readyToFire;
        private float timer = 0f;

        protected void OnEnable()
        {
            OnEnemySpawned(this);
            target = transform.position;
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
        private void Update()
        {
            if (!readyToFire) Reload();

            if (readyToFire && PlayerIsVisible() && PlayerIsInRange()) Shoot();
        }
        private void FixedUpdate()
        {
            if (PlayerIsVisible()) target = player.transform.position;

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
            var proj = ProjectilePool.GetEnemyProjectile();
            Physics.IgnoreCollision(coll, proj.GetComponent<Collider>());
            proj.gameObject.SetActive(true);
            proj.transform.position = shootPos.position;
            proj.Initialize(target, force, 1);

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
        private bool PlayerIsVisible()
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
