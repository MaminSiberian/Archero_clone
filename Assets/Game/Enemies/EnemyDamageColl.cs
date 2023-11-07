using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Collider))]
    public class EnemyDamageColl : MonoBehaviour
    {
        [SerializeField] private float damage = 1;
        [SerializeField] private bool selfDestructable;

        private Collider coll;

        private void Awake()
        {
            coll = GetComponent<Collider>();
            coll.isTrigger = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GetDamage(damage);
            }

            gameObject.SetActive(!selfDestructable);          
        }
    }
}
