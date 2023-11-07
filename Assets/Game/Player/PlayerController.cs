using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(PlayerModel))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask wallsLayer;
    public static event Action OnPlayerDeathEvent;

    private Rigidbody rb;
    private PlayerModel model;
    private InputManager input;

    private Vector2 inputSpeed;
    private float currentHP;
    private List<EnemyBase> enemiesOnLevel = new List<EnemyBase>();
    private Transform target;
    private const int wallsLayerNumber = 6;

    #region monobehs
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        model = GetComponent<PlayerModel>();
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
    }
    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    #endregion

    private void Move()
    {
        rb.velocity = new Vector3(inputSpeed.x, 0f, inputSpeed.y) * model.speed * 100 * Time.fixedDeltaTime;
    }
    private void Rotate()
    {
        if (Mathf.Abs(inputSpeed.x) <= 0.2f & Mathf.Abs(inputSpeed.y) <= 0.2f)
        {
            LookForEnemies();
            transform.LookAt(target);
        }
        else
        {
            transform.LookAt(transform.position + new Vector3(inputSpeed.x, 0f, inputSpeed.y));
        }
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
    }
    private void RemoveEnemy(EnemyBase enemy)
    {
        enemiesOnLevel.Remove(enemy);
    }
    #endregion
}
