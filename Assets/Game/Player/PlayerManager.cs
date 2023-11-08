using UnityEngine;

public enum Weapon
{
    Pistol,
    Shotgun,
    Basooka
}

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    public Weapon weapon => _weapon;
    public static PlayerManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void ChangeWeapon(Weapon weapon)
    {
        this._weapon = weapon;
    }
}
