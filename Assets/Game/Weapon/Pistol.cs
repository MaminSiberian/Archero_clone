
public class Pistol : WeaponBase
{
    public override Weapon name => Weapon.Pistol;
    public override float damage => 1f;

    public override float reloadingTime => 0.8f;

    public override float force => 20;

    public override float numberOfProj => 1;
}
