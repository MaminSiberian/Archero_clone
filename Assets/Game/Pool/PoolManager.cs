using UnityEngine;
using Enemies;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private int poolCount = 10;
    [SerializeField] private PistolProjectile pistolProjPrefab;
    [SerializeField] private ShotgunProjectile shotgunProjPrefab;
    [SerializeField] private BasookaProjectile basookaProjPrefab;

    [SerializeField] private EnemyProjectile enemyProjPrefab;
    [SerializeField] private Destructor deathEffectPrefab;
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private Explosion explosionPrefab;

    private static Pool<PistolProjectile> pistolPool;
    private static Pool<ShotgunProjectile> shotgunPool;
    private static Pool<BasookaProjectile> basookaPool;

    private static Pool<EnemyProjectile> enemyPool;
    private static Pool<Destructor> deathEffectPool;
    private static Pool<Coin> coinPool;
    public static Pool<Explosion> explosionPool;

    private void Start()
    {
        pistolPool = new Pool<PistolProjectile>(pistolProjPrefab, poolCount, transform);
        shotgunPool = new Pool<ShotgunProjectile>(shotgunProjPrefab, poolCount, transform);
        basookaPool = new Pool<BasookaProjectile>(basookaProjPrefab, poolCount, transform);

        enemyPool = new Pool<EnemyProjectile>(enemyProjPrefab, poolCount, transform);
        deathEffectPool = new Pool<Destructor>(deathEffectPrefab, poolCount, transform);
        coinPool = new Pool<Coin>(coinPrefab, poolCount, transform);
        explosionPool = new Pool<Explosion>(explosionPrefab, poolCount, transform);
    }

    public static PistolProjectile GetPistolProjectile()
    {
        return pistolPool.GetObject();
    }
    public static ShotgunProjectile GetShotgunProjectile()
    {
        return shotgunPool.GetObject();
    }
    public static BasookaProjectile GetBasookaProjectile()
    {
        return basookaPool.GetObject();
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
