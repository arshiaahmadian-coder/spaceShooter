using UnityEngine;

public class ShooterRocket : MonoBehaviour
{
    [SerializeField] GameObject rocketBullet;
    [SerializeField] Transform firePos;

    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] int bulletDamage;
    [SerializeField] float fireRate = 0.15f;
    [SerializeField] float minDistanceToShoot;
    [SerializeField] AudioClip shootClip;
    
    PlayerHealth player;
    private float timer;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (minDistanceToShoot < distance) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            timer = fireRate;
        }
    }
    
    private void Shoot()
    {
        GameObject b = Instantiate(rocketBullet, firePos.position, firePos.rotation);
        b.GetComponent<EnemyBullet>().speed = bulletSpeed;
        b.GetComponent<EnemyBullet>().damage = bulletDamage;

        AudioManager.instance.PlayLowSFX(shootClip);
    }
}
