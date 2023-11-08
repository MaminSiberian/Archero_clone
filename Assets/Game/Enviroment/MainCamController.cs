using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MainCamController : MonoBehaviour
{
    private Camera cam;
    private LevelConfig config;

    private const float fowCoeff = 6.5f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void Start()
    {
        config = FindObjectOfType<LevelConfig>();
        cam.fieldOfView = config.arenaSize.x * fowCoeff;
    }
}
