using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    [SerializeField] AudioClip explosionClip;
    [SerializeField] Transform explosionPos;
    [SerializeField] GameObject explosionFX;
    [SerializeField] Slider bossHeathBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            currentHealth--;
            bossHeathBar.value = currentHealth / 100f;
            if (currentHealth <= 0) Die();
        }
    }

    private void Die()
    {
        Instantiate(explosionFX, explosionPos.position, Quaternion.identity);
        WinLoseManager.instance.DeleteEnemyInList(this.gameObject);
        AudioManager.instance.PlaySFX(explosionClip);
        bossHeathBar.value = bossHeathBar.minValue;
        Destroy(gameObject);
    }
}
