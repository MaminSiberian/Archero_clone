using TMPro;

namespace UI
{
    public class WeaponButton : ButtonBase
    {
        private TextMeshProUGUI text;
        private Weapon weapon;
        private PlayerManager playerManager;

        protected override void Awake()
        {
            base.Awake();
            text = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Start()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            weapon = PlayerManager.weapon.name;
            text.text = weapon.ToString();
        }

        protected override void OnButtonClick()
        {
            switch (weapon)
            {
                case Weapon.Pistol:
                    playerManager.ChangeWeapon(new Shotgun());
                    weapon = Weapon.Shotgun;
                    break;
                case Weapon.Shotgun:
                    playerManager.ChangeWeapon(new Basooka());
                    weapon = Weapon.Basooka;
                    break;
                case Weapon.Basooka:
                    playerManager.ChangeWeapon(new Pistol());
                    weapon = Weapon.Pistol;
                    break;
                default: 
                    break;
            }
            text.text = weapon.ToString();
        }
    }
}
