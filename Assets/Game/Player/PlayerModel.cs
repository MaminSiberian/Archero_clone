using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxHP;
    [SerializeField] private float _projSpeed;
    [SerializeField] private float _reloadTime;

    public float moveSpeed => _moveSpeed;
    public float maxHP => _maxHP;
    public float projSpeed => _projSpeed;
    public float reloadTime => _reloadTime;
}
