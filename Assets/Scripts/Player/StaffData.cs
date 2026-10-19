using UnityEngine;

[CreateAssetMenu(fileName = "CajadoNovo", menuName = "Cajado")]
public class StaffData : ScriptableObject
{
    [SerializeField]Sprite sprite;
    [SerializeField]Projectile projectile;
    [SerializeField]AudioClip shotSFX;
    [SerializeField]int projectileDamage = 10;
    [SerializeField]float projectileSpeed = 13f;
    [SerializeField]float shotCooldown = 0.3f;

    public Sprite StaffSprite => sprite;
    public Projectile ProjectilePrefab => projectile;
    public AudioClip ShotSFX => shotSFX;
    public int ProjectileDamage => projectileDamage;
    public float ProjectileSpeed => projectileSpeed;
    public float ShotCooldown => shotCooldown;

}