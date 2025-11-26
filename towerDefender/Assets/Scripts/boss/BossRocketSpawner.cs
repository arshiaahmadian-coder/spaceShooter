using UnityEngine;

public class BossRocketSpawner : MonoBehaviour
{
    [SerializeField] float spawnRate;
    private float timer;
    [SerializeField] Transform spawnPos1;
    [SerializeField] Transform spawnPos2;
    [SerializeField] GameObject rocket;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnRocket();
            timer = spawnRate;
        }
    }

    private void SpawnRocket()
    {
        GameObject a = Instantiate(rocket, spawnPos1.position, spawnPos1.rotation);
        GameObject b = Instantiate(rocket, spawnPos2.position, spawnPos2.rotation);
    }
}
