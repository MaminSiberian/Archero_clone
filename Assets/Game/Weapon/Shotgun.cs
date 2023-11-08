
public class Shotgun : WeaponBase
{
    public override Weapon name => Weapon.Shotgun;
    public override float damage => 0.7f;

    public override float reloadingTime => 1.1f;

    public override float force => 20;

    public override float numberOfProj => 4;
}
