using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxHP;

    public float speed => _speed;
    public float maxHP => _maxHP;
}
