using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtn : MonoBehaviour
{
    public void OnStartBtnClick()
    {
        MusicPlayer.instance.StartMusic();
        SceneManager.LoadScene(GameManager.instance.currentLevelIndex);
    }
}
