using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    [SerializeField] private Vector2Int _arenaSize;

    public Vector2Int arenaSize => _arenaSize;
}
