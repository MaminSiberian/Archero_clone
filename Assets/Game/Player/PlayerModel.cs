using UI;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxHP;

    public float moveSpeed => _moveSpeed;
    public float maxHP => _maxHP;

    public WeaponBase weapon { get; private set; }

    private void Start()
    {
        weapon = PlayerManager.weapon;
    }
}
