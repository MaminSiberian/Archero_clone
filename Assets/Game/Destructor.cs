using UnityEngine;

public class Destructor : MonoBehaviour
{
    [SerializeField] private float lifetime;

    private float timer = 0f;
    private void Update()
    {
        if (timer >= lifetime)
        {
            timer = 0f;
            gameObject.SetActive(false);
        }
        else
            timer += Time.deltaTime;
    }
}
