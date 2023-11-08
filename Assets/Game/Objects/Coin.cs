using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Collider))]
public class Coin : MonoBehaviour
{
    [SerializeField] private float magnitDistance = 5f;
    [SerializeField] private float speed = 10f;
    public static event Action OnCoinCollectedEvent;

    private Rigidbody rb;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.position) <= magnitDistance)
        {
            rb.MovePosition(transform.position + (player.position - transform.position).normalized * speed * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            OnCoinCollectedEvent?.Invoke();
            Deactivate();
        }
    }
    
    private void Deactivate()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
