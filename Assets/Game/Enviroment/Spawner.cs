using Enemies;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private GameObject door;

    private float arenaSizeX;
    private float arenaSizeZ;

    private void Awake()
    {
        LevelConfig conf = FindObjectOfType<LevelConfig>();

        arenaSizeX = conf.arenaSize.x;
        arenaSizeZ = conf.arenaSize.y;

        door.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        EnemyBase.OnEnemySpawnedEvent += RelocateEnemy;
        PlayerController.OnEnemiesDefeatedEvent += SpawnDoor;
    }
    private void OnDisable()
    {
        EnemyBase.OnEnemySpawnedEvent -= RelocateEnemy;
        PlayerController.OnEnemiesDefeatedEvent -= SpawnDoor;
    }
    private void Start()
    {
        RelocatePlayer();
    }
    private void RelocateEnemy(EnemyBase enemy)
    {
        float x = Random.Range((-arenaSizeX / 2 + 1), (arenaSizeX / 2 - 1));
        float z = Random.Range((-arenaSizeZ / 3 + 2), (arenaSizeZ / 3 - 2));

        enemy.transform.position = new Vector3
            (enemySpawnPoint.position.x + x,
            enemy.transform.position.y,
            enemySpawnPoint.position.z + z);
    }
    private void RelocatePlayer()
    {
        var player = FindObjectOfType<PlayerController>();
        player.transform.position = new Vector3
            (playerSpawnPoint.position.x,
            player.transform.position.y,
            playerSpawnPoint.position.z);
    }
    private void SpawnDoor()
    {
        door.gameObject.SetActive(true);
    }
}
