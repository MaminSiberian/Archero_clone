using Enemies;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float arenaSizeX;
    private float arenaSizeZ;

    private void Awake()
    {
        LevelConfig conf = FindObjectOfType<LevelConfig>();

        arenaSizeX = conf.arenaSize.x;
        arenaSizeZ = conf.arenaSize.y;
    }
    private void OnEnable()
    {
        EnemyBase.OnEnemySpawnedEvent += RelocateEnemy;
    }
    private void OnDisable()
    {
        EnemyBase.OnEnemySpawnedEvent -= RelocateEnemy;
    }
    private void RelocateEnemy(EnemyBase enemy)
    {
        float x = Random.Range((-arenaSizeX / 2 + 1), (arenaSizeX / 2 - 1));
        float z = Random.Range((-arenaSizeZ / 3 + 2), (arenaSizeZ / 3 - 2));

        enemy.transform.position = new Vector3
            (transform.position.x + x,
            enemy.transform.position.y,
            transform.position.z + z);
    }
}
