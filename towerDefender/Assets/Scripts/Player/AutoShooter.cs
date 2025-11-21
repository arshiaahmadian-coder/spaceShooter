using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public TurretAiming turret;
    public GameObject bulletPrefab;
    public GameObject player;
    public float bulletSpeed = 10f;
    public float fireRate = 0.15f;
    private float timer;
    [SerializeField] AudioClip shootClip;

    private void Update()
    {
        if (
            turret.aimJoystick.IsHolding &&
            turret.AimDirection.sqrMagnitude > 0.01f &&
            turret.canShoot
        ) {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Shoot();
                timer = fireRate;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void Shoot()
    {
        AudioManager.instance.PlayLowSFX(shootClip);
        // GameObject b = Instantiate(bulletPrefab, turret.firePoint.position, Quaternion.identity);
        // GameObject b = Instantiate(bulletPrefab, turret.firePoint.position, turret.firePoint.rotation);
        GameObject b = Instantiate(bulletPrefab, turret.firePoint.position, player.transform.rotation);

        b.transform.right = turret.AimDirection.normalized;

        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.linearVelocity = turret.AimDirection.normalized * bulletSpeed;
    }

}
