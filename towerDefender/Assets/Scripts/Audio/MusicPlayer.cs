using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;

    public static MusicPlayer instance;
    private void Awake()
    {
        instance = this;
    }

    public void StartMusic()
    {
        AudioManager.instance.PlayMusic(musicClip);
    }
}
