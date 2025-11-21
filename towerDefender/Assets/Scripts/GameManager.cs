using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevelIndex = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    
    public void IncreaseLevelIndex()
    {
        currentLevelIndex++;
        // TODO: save currentLevelIndex value in long storage
    }
}
