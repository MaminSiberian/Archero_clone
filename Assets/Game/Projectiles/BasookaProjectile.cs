using UnityEngine;

public class BasookaProjectile : MonoBehaviour
{
    private Rigidbody rb;

    private const float deviation = 0.7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        var exp = PoolManager.GetExplosion();
        exp.transform.position = transform.position;
        exp.gameObject.SetActive(true);

        Deactivate();
    }

    public void Initialize(Vector3 target, float force)
    {
        rb.AddForce((target - transform.position).normalized * force * 15);
    }
    private void Deactivate()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
