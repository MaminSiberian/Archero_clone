using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(PlayerModel))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerModel model;
    private InputManager input;

    private Vector2 inputSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        model = GetComponent<PlayerModel>();
    }
    private void Start()
    {
        input = FindObjectOfType<InputManager>();
    }
    private void Update()
    {
        inputSpeed = input.speed;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        rb.velocity = new Vector3(inputSpeed.x, 0f, inputSpeed.y) * model.speed * Time.fixedDeltaTime;
    }
}
