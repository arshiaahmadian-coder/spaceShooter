using UnityEngine;

public class ShipEngineSound : MonoBehaviour
{
    Joystick joystick;
    public AudioSource engineAudio;
    public float fadeSpeed;
    public float maxVolume;

    private bool isMoving = false;

    void Start()
    {
        joystick = GameObject.Find("move joystick").GetComponent<Joystick>(); 
    }

    void Update()
    {
        bool joystickMoved = Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f;

        if (joystickMoved && !isMoving)
        {
            isMoving = true;
            if (!engineAudio.isPlaying)
                engineAudio.Play();
        }
        else if (!joystickMoved && isMoving)
        {
            isMoving = false;
        }

        float targetVolume = isMoving ? maxVolume : 0f;
        engineAudio.volume = Mathf.MoveTowards(engineAudio.volume, targetVolume, fadeSpeed * Time.deltaTime);
        engineAudio.pitch = Mathf.Lerp(1.2f, 1.5f, joystick.Direction.magnitude);

        if (!isMoving && engineAudio.volume <= 0.01f)
        {
            engineAudio.Stop();
        }
    }
}
