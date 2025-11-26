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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("CamBound")) {
            Destroy(gameObject);
        }
    }
}
