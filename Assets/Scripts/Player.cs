using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    int inputX;
    public int defSpeed;
    float speed;
    public int dashSpeed;
    public float dashSlow;
    bool isGrounded, isDashing = false;
    public float CT;
    float TLG; // Time left ground
    [SerializeField] Rigidbody2D rb;

    void Update()
    {
        GetInput();
        Jump();
        Dash();
    }

    private void FixedUpdate()
    {
        DashSlow();
        Move();
        DashSlow();
    }

    void GetInput()
    {
        if (!isDashing)
        {
            inputX = (Keyboard.current.dKey.isPressed ? 1 : 0) + (Keyboard.current.aKey.isPressed ? -1 : 0); // Left & Right movement
        }
    }

    void Move()
    {
        if (isDashing)
        {
            rb.gravityScale = 0;
            rb.linearVelocityY = 0;
        }
        else
        {
            rb.gravityScale = 3;
        }
        rb.linearVelocityX = inputX * speed * Time.fixedDeltaTime * 100; // Left & Right movement
    }

    void Jump()
    {
        
        if (Keyboard.current.spaceKey.isPressed && isGrounded)
        {
            rb.linearVelocityY = 10;
            isGrounded = false;
        }
    }

    void Dash()
    {
        if (Keyboard.current.shiftKey.wasPressedThisFrame && !isDashing && inputX != 0)
        {
            speed = dashSpeed;
            isDashing = true;
        }
    }

    void DashSlow()
    {
        if (isDashing)
        {
            speed -= dashSlow * Time.fixedDeltaTime * 10;
        }
        if (speed < defSpeed)
        {
            isDashing = false;
            speed = defSpeed;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}
}
