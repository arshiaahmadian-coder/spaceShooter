using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SmartEnemy : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 5f;
    public float fleeSpeed = 3f;
    public float attackDistance = 10f;
    public float minDistanceToFlee = 8f;

    private Rigidbody2D rb;
    private bool isAttacking = false;
    private float timer;
    public float fireRate;

    [SerializeField] GameObject rocketBullet;
    [SerializeField] Transform firePos;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] int bulletDamage;
    [SerializeField] AudioClip shootClip;

    [SerializeField] private int damageAmount; 
    [SerializeField] GameObject explosionFX;
    [SerializeField] Transform explosionPos;
    [SerializeField] AudioClip explosionClip;

    [SerializeField] private int maxHealth;
    private int currentHealth;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        currentHealth = maxHealth;
    }

    void Update()
    {
        if(isAttacking)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Shoot();
                timer = fireRate;
            }
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 currentPos = rb.position;
        Vector2 playerPos = player.position;
        float distance = Vector2.Distance(currentPos, playerPos);
        Vector2 direction = (playerPos - currentPos).normalized;

        if (distance > attackDistance)
        {
            MoveTo(currentPos + direction * chaseSpeed * Time.fixedDeltaTime);
            isAttacking = false;
        }
        else if (distance <= attackDistance && !isAttacking)
        {
            isAttacking = true;
            rb.linearVelocity = Vector2.zero;
        }
        else if (distance < minDistanceToFlee)
        {
            Vector2 fleeTarget = currentPos - direction * fleeSpeed * Time.fixedDeltaTime;
            MoveTo(fleeTarget);
            isAttacking = false;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void MoveTo(Vector2 targetPosition)
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, chaseSpeed * Time.fixedDeltaTime));
    }

    private void Shoot()
    {
        GameObject b = Instantiate(rocketBullet, firePos.position, firePos.rotation);
        b.GetComponent<EnemyBullet>().speed = bulletSpeed;
        b.GetComponent<EnemyBullet>().damage = bulletDamage;

        AudioManager.instance.PlayLowSFX(shootClip);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth.instance.TakeDamage(damageAmount);
            Die();
        }
        else
            AudioManager.instance.PlaySFX(explosionClip);

        if(other.gameObject.tag == "PlayerBullet")
        {
            currentHealth--;
            if (currentHealth <= 0) Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            currentHealth--;
            if (currentHealth <= 0) Die();
        }
    }

    void Die()
    {
        Instantiate(explosionFX, explosionPos.position, Quaternion.identity);
        WinLoseManager.instance.DeleteEnemyInList(this.gameObject);
        AudioManager.instance.PlaySFX(explosionClip);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minDistanceToFlee);
    }
}