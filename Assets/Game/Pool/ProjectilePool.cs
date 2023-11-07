using UnityEngine;
using Enemies;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private int poolCount = 10;
    [SerializeField] private PlayerProjectile playerProjPrefab;
    [SerializeField] private EnemyProjectile enemyProjPrefab;

    private static Pool<PlayerProjectile> playerPool;
    private static Pool<EnemyProjectile> enemyPool;

    private void Start()
    {
        playerPool = new Pool<PlayerProjectile>(playerProjPrefab, poolCount, transform);
        enemyPool = new Pool<EnemyProjectile>(enemyProjPrefab, poolCount, transform);
    }

    public static PlayerProjectile GetPlayerProjectile()
    {
        return playerPool.GetObject();
    }
    public static EnemyProjectile GetEnemyProjectile()
    {
        return enemyPool.GetObject();
    }
}
