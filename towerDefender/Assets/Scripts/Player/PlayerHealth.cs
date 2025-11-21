using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    [SerializeField] List<GameObject> healthObjList;
    [SerializeField] AudioClip playerHitClip;

    // Singleton
    public static PlayerHealth instance;
    void Awake() { instance = this; }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        AudioManager.instance.PlaySFX(playerHitClip);
        currentHealth -= damageAmount;
        try
        {
            healthObjList[currentHealth].SetActive(false);
            healthObjList.RemoveAt(currentHealth);
        } catch
        {
            print("all of hurts are removed");
        }

        // screen shake
        FindFirstObjectByType<ScreenShake>().Shake(2f, 4f, 0.3f);

        if(currentHealth <= 0)
        {
            // call lose func
            WinLoseManager.instance.CallLose();
        }
    }
}
