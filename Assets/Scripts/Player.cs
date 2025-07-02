using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    int input;
    public int speed;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        
    }

    void Update()
    {
        GetInput();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        input = (Keyboard.current.dKey.isPressed? 1 : 0) + (Keyboard.current.aKey.isPressed ? -1 : 0); // Left & Right movement
    }

    void Move()
    {
        rb.linearVelocityX = input * speed * Time.fixedDeltaTime * 100; // Left & Right movement
    }

    void Jump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocityY = 10;
        }
    }
}
