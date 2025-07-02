using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    int input;
    public int speed;
    bool grounded;
    public float CT;
    float TLG; // Time left ground
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
        
        if (Keyboard.current.spaceKey.isPressed && grounded)
        {
            rb.linearVelocityY = 10;
            grounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}
