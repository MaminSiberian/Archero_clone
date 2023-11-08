using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MainCamController : MonoBehaviour
{
    private Camera cam;
    private LevelConfig config;

    private const float fowCoeff = 7;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void Start()
    {
        config = FindObjectOfType<LevelConfig>();
        cam.fieldOfView = config.arenaSize.x * 7;
    }
}
