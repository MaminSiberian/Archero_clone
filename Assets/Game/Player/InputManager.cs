using UnityEngine;

public enum InputType 
{
    PC,
    Touch
}

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputType inputType;

    public Vector2 speed {  get; private set; }
    private Joystick joystick;
    
    private void Start()
    {
        if (inputType == InputType.Touch) joystick = FindObjectOfType<Joystick>();
    }
    private void Update()
    {
        GetInput();
    }
    private void GetInput()
    {
        switch (inputType)
        {
            case InputType.PC:
                speed = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                break;
            case InputType.Touch:
                speed = new Vector2(joystick.Horizontal, joystick.Vertical);
                break;
            default:
                break;
        }
    }
}
