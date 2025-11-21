using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public int damage;
    [SerializeField] int lifeTime;

    void Start()
    {
        DestroyBullet(lifeTime);
    }

    void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void DestroyBullet(int t)
    {
        Destroy(gameObject, t);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth.instance.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
