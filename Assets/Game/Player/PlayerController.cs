using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider)), RequireComponent(typeof(PlayerModel))]
public class PlayerController : MonoBehaviour, IDamagable
{
    [SerializeField] private Transform shootPos;
    [SerializeField] private LayerMask wallsLayer;

    public static event Action<float> OnPlayerHealthChangedEvent;
    public static event Action OnPlayerDeathEvent;
    public static event Action OnEnemiesDefeatedEvent;

    private Rigidbody rb;
    private Collider coll;
    private PlayerModel model;
    private InputManager input;

    private Vector2 inputSpeed;
    private float currentHP;
    private List<EnemyBase> enemiesOnLevel = new List<EnemyBase>();
    private Transform target;
    private const int wallsLayerNumber = 6;

    private float timer = 0f;
    private bool readyToFire;

    #region monobehs
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        model = GetComponent<PlayerModel>();
        currentHP = model.maxHP;
    }
    private void OnEnable()
    {
        EnemyBase.OnEnemySpawnedEvent += AddEnemy;
        EnemyBase.OnEnemyDeathEvent += RemoveEnemy;
    }
    private void OnDisable()
    {
        EnemyBase.OnEnemySpawnedEvent -= AddEnemy;
        EnemyBase.OnEnemyDeathEvent -= RemoveEnemy;
    }
    private void Start()
    {
        input = FindObjectOfType<InputManager>();
    }
    private void Update()
    {
        inputSpeed = input.speed;
        if (!readyToFire) Reload();

        if (readyToFire && target != null) Shoot();
    }
    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    #endregion

    private void Move()
    {
        rb.velocity = new Vector3(inputSpeed.x, 0f, inputSpeed.y) * model.moveSpeed * 100 * Time.fixedDeltaTime;
    }
    private void Rotate()
    {
        if (Mathf.Abs(inputSpeed.x) <= 0.2f && Mathf.Abs(inputSpeed.y) <= 0.2f && enemiesOnLevel.Count > 0)
        {
            LookForEnemies();
        }
        else
        {
            target = null;
            transform.LookAt(transform.position + new Vector3(inputSpeed.x, 0f, inputSpeed.y));
        }
    }
    private void Shoot()
    {
        if (target == null) return;

        switch (model.weapon.name)
        {
            case Weapon.Pistol:
                var pistolProj = PoolManager.GetPistolProjectile();
                Physics.IgnoreCollision(coll, pistolProj.GetComponent<Collider>());
                pistolProj.gameObject.SetActive(true);
                pistolProj.transform.position = shootPos.position;
                pistolProj.Initialize(target, model.weapon.force, model.weapon.damage);
                break;
            case Weapon.Shotgun:
                for (int i = 0; i < 4; i++)
                {
                    var shotgunProj = PoolManager.GetShotgunProjectile();
                    Physics.IgnoreCollision(coll, shotgunProj.GetComponent<Collider>());
                    shotgunProj.gameObject.SetActive(true);
                    shotgunProj.transform.position = shootPos.position;
                    shotgunProj.Initialize(target.position, model.weapon.force, model.weapon.damage);
                }
                break;
            case Weapon.Basooka:
                var basookaProj = PoolManager.GetBasookaProjectile();
                Physics.IgnoreCollision(coll, basookaProj.GetComponent<Collider>());
                basookaProj.gameObject.SetActive(true);
                basookaProj.transform.position = shootPos.position;
                basookaProj.Initialize(target.position, model.weapon.force);
                break;
            default:
                break;
        }

        readyToFire = false;
    }
    private void Reload()
    {
        if (timer >= model.weapon.reloadingTime)
        {
            timer -= model.weapon.reloadingTime;
            readyToFire = true;
        }
        else
            timer += Time.deltaTime;
    }

    public void GetDamage(float value)
    {
        currentHP -= value;
        OnPlayerHealthChangedEvent?.Invoke(currentHP);

        if (currentHP <= 0)
        {
            currentHP = model.maxHP;
            KillPlayer();
        }
    }
    private void KillPlayer()
    {
        OnPlayerDeathEvent?.Invoke();
        gameObject.SetActive(false);
    }

    #region enemies
    private void LookForEnemies()
    {
        target = null;
        if (enemiesOnLevel == null) return;

        enemiesOnLevel = enemiesOnLevel.OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position)).ToList();

        foreach (var enemy in enemiesOnLevel)
        {
            if (EnemyIsVisible(enemy))
            {
                target = enemy.transform;
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                return;
            }
        }
    }
    private bool EnemyIsVisible(EnemyBase enemy)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, enemy.transform.position, out hit, wallsLayer))
        {
            if (hit.collider.gameObject.layer == wallsLayerNumber)
            {
                return false;
            }
        }
        return true;
    }
    private void AddEnemy(EnemyBase enemy)
    {
        enemiesOnLevel.Add(enemy);
        enemiesOnLevel = enemiesOnLevel.Distinct().ToList();
    }
    private void RemoveEnemy(EnemyBase enemy)
    {
        enemiesOnLevel.Remove(enemy);
        if (enemiesOnLevel.Count <= 0)
        {
            OnEnemiesDefeatedEvent?.Invoke();
        }
    }
    #endregion
}
