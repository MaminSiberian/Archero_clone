public enum Weapon
{
    Pistol,
    Shotgun,
    Basooka
}

public abstract class WeaponBase
{
    public abstract Weapon name { get; }
    public abstract float damage { get; }
    public abstract float reloadingTime { get; }
    public abstract float force { get; }
    public abstract float numberOfProj { get; }
}
