using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick moveJoystick;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveJoystick = GameObject.Find("move joystick").GetComponent<Joystick>();   
    }

    private void Update()
    {
        direction = moveJoystick.Direction;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction.normalized * moveSpeed;
    }
}
