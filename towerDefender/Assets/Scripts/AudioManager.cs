using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Mixer")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource lowSfxSource;
    public AudioSource midSfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float pitchRandom = 0.2f)
    {
        sfxSource.pitch = 1 + UnityEngine.Random.Range(-pitchRandom, pitchRandom);
        sfxSource.PlayOneShot(clip);
    }

    public void PlayLowSFX(AudioClip clip, float pitchRandom = 0.2f)
    {
        lowSfxSource.pitch = 1 + UnityEngine.Random.Range(-pitchRandom, pitchRandom);
        lowSfxSource.PlayOneShot(clip);
    }

    public void PlayMidSFX(AudioClip clip, float pitchRandom = 0.2f)
    {
        midSfxSource.pitch = 1 + UnityEngine.Random.Range(-pitchRandom, pitchRandom);
        midSfxSource.PlayOneShot(clip);
    }
}
