using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int lifeTime;

    void Start()
    {
        DestroyBullet(lifeTime);
    }

    private void DestroyBullet(int t)
    {
        Destroy(gameObject, t);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
