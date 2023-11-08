using UnityEngine;
using Enemies;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private int poolCount = 10;
    [SerializeField] private PlayerProjectile playerProjPrefab;
    [SerializeField] private EnemyProjectile enemyProjPrefab;
    [SerializeField] private Destructor deathEffectPrefab;
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private Explosion explosionPrefab;

    private static Pool<PlayerProjectile> playerPool;
    private static Pool<EnemyProjectile> enemyPool;
    private static Pool<Destructor> deathEffectPool;
    private static Pool<Coin> coinPool;
    public static Pool<Explosion> explosionPool;

    private void Start()
    {
        playerPool = new Pool<PlayerProjectile>(playerProjPrefab, poolCount, transform);
        enemyPool = new Pool<EnemyProjectile>(enemyProjPrefab, poolCount, transform);
        deathEffectPool = new Pool<Destructor>(deathEffectPrefab, poolCount, transform);
        coinPool = new Pool<Coin>(coinPrefab, poolCount, transform);
        explosionPool = new Pool<Explosion>(explosionPrefab, poolCount, transform);
    }

    public static PlayerProjectile GetPlayerProjectile()
    {
        return playerPool.GetObject();
    }
    public static EnemyProjectile GetEnemyProjectile()
    {
        return enemyPool.GetObject();
    }
    public static Destructor GetDeathEffect()
    {
        return deathEffectPool.GetObject();
    }
    public static Coin GetCoin()
    {
        return coinPool.GetObject();
    }
    public static Explosion GetExplosion()
    {
        return explosionPool.GetObject();
    }
}
