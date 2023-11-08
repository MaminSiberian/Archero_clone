using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Door : MonoBehaviour
{
    private void Awake()
    {
        Collider coll = GetComponent<Collider>();
        coll.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().LoadNextLevel();
        }
    }
}
