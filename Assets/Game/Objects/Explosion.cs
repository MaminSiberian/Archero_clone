using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Explosion : MonoBehaviour
{
    private float damage = 1;

    private void Awake()
    {
        Collider coll = GetComponent<Collider>();
        coll.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagable obj = other.GetComponent<IDamagable>();

        if (obj != null)
        {
            obj.GetDamage(damage);
        }
    }
}
