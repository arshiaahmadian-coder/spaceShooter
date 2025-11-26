using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [SerializeField] private int damageAmount; 
    [SerializeField] GameObject explosionFX;
    [SerializeField] Transform explosionPos;
    [SerializeField] AudioClip explosionClip;
    
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindFirstObjectByType<PlayerMovement>().transform;
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        rb.linearVelocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth.instance.TakeDamage(damageAmount);
        }
        else
            AudioManager.instance.PlaySFX(explosionClip);

        Die();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            Die();
            AudioManager.instance.PlaySFX(explosionClip);
        }
    }

    void Die()
    {
        Instantiate(explosionFX, explosionPos.position, Quaternion.identity);
        WinLoseManager.instance.DeleteEnemyInList(this.gameObject);
        Destroy(gameObject);
    }
}
