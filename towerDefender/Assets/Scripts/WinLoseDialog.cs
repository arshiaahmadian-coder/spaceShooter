using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseDialog : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene(GameManager.instance.currentLevelIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(GameManager.instance.currentLevelIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
