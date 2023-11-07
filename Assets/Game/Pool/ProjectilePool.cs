using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private int poolCount = 10;
    [SerializeField] private PlayerProjectile prefab;

    private static Pool<PlayerProjectile> pool;

    private void Start()
    {
        pool = new Pool<PlayerProjectile>(this.prefab, this.poolCount, this.transform);
    }

    public static PlayerProjectile GetPlayerProjectile()
    {
        return pool.GetObject();
    }
}
