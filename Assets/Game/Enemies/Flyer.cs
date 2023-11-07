using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class Flyer : EnemyBase
    {
        [SerializeField] private float altitude;
        [SerializeField] private float reloadTime;
        [SerializeField] private float shootingRange;
        [SerializeField] private float force;

        [SerializeField] private Transform shootPos;

        private Vector3 target;
        private bool readyToFire;
        private float timer = 0f;

        protected void OnEnable()
        {
            OnEnemySpawned(this);
            transform.position = new Vector3(transform.position.x, altitude, transform.position.z);
            target = transform.position;
        }
        protected void OnDisable()
        {
            OnEnemyDeath(this);
        }
        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
        }
        protected override void Start()
        {
            base.Start();
            OnEnemySpawned(this);
        }
        private void Update()
        {
            if (PlayerIsVisible())
            {
                target = new Vector3(player.transform.position.x, altitude, player.transform.position.z);
                transform.LookAt(target);
            }

            if (!readyToFire && PlayerIsVisible()) Reload();

            if (readyToFire && PlayerIsVisible() && PlayerIsInRange()) Shoot();
        }
        private void FixedUpdate()
        {
            if (PlayerIsInRange() && PlayerIsVisible())
            {
                return;
            }
            Move();
        }
        private void Move()
        {
            rb.MovePosition(transform.position + (target - transform.position).normalized * moveSpeed * Time.fixedDeltaTime);
        }
        private void Shoot()
        {
            var proj = ProjectilePool.GetEnemyProjectile();
            Physics.IgnoreCollision(coll, proj.GetComponent<Collider>());
            proj.gameObject.SetActive(true);
            proj.transform.position = shootPos.position;
            proj.Initialize(player.transform.position, force, damage);

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
