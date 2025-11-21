using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelBtn : MonoBehaviour
{
    public void NextLevel()
    {
        GameManager.instance.IncreaseLevelIndex();
        SceneManager.LoadScene(GameManager.instance.currentLevelIndex);
    }
}
