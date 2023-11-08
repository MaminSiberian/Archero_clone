using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static WeaponBase weapon { get; private set; }

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

        if (weapon == null) weapon = new Pistol();
    }
    public void ChangeWeapon(WeaponBase _weapon)
    {
        weapon = _weapon;
    }
}
