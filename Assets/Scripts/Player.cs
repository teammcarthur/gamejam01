using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    int inputX, inputY, paintCollected;
    public int defSpeed, dashSpeed, climbSpeedX, climbSpeedY;
    float speed;
    public float dashSlow;
    bool isGrounded, isDashing, isClimbing, canDash = false;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;
    Vector2 spawnPos;
    public AudioResource scaryMusic;
    public AudioSource Music, vines, jump, dash, collect, death;
    public GameObject pauseMenu, skybox;

    void Awake()
    {
        spawnPos = transform.position;
    }

    void Update()
    {
        GetInput();
        Jump();
        Dash();
        Restarting();
        Animate();
        Menu();
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
        if (isClimbing && !isDashing)
        {
            inputY = (Keyboard.current.wKey.isPressed ? 1 : 0) + (Keyboard.current.sKey.isPressed ? -1 : 0); // Up and down movement - on vines
        }
    }

    void Move()
    {
        if (isDashing)
        {
            rb.gravityScale = 0;
            rb.linearVelocityY = 0;
        }
        else if (!isClimbing)
        {
            rb.gravityScale = 3;
        }
        else if (isClimbing)
        {
            speed = climbSpeedX;
            rb.gravityScale = 0;
            rb.linearVelocityY = inputY * climbSpeedY * Time.fixedDeltaTime * 100; // Up & Down movement - Climbing vines
        }
        rb.linearVelocityX = inputX * speed * Time.fixedDeltaTime * 100; // Left & Right movement
    }

    void Jump()
    {
        
        if (Keyboard.current.spaceKey.isPressed && isGrounded)
        {
            jump.Play();
            rb.linearVelocityY = 10;
            isGrounded = false;
            canDash = true;
        }
    }

    void Dash()
    {
        if (Keyboard.current.shiftKey.wasPressedThisFrame && !isDashing && inputX != 0 && canDash)
        {
            dash.Play();
            speed = dashSpeed;
            isDashing = true;
            canDash = false;
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

    void Animate()
    {
        if (rb.linearVelocityX == 0)
        {
            animator.SetTrigger("Stand");
        }
        else
        {
            animator.SetTrigger("Run");
        }

        if (rb.linearVelocityX > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            if (rb.linearVelocityX < 0)
            {
                sprite.flipX = false;
            }
        }
    }

    void Restarting()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            transform.position = spawnPos;
        }
    }

    void Menu()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            pauseMenu.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDash = true;
        }
        if (collision.gameObject.CompareTag("Vines") && !canDash)
        {
            canDash = true;
        }
        if (isClimbing && !vines.isPlaying)
        {
            vines.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vines"))
        {
            isClimbing = true;
            canDash = true;
        }
        if (collision.gameObject.CompareTag("Death"))
        {
            death.Play();
            transform.position = spawnPos;
        }
        if (collision.gameObject.CompareTag("Paint"))
        {
            collect.Play();
            paintCollected += 1;
            spawnPos = collision.gameObject.transform.position;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("EndBox") && paintCollected == 5)
        {
            SceneManager.LoadScene("EndCutscene");
        }
        if (collision.gameObject.CompareTag("Music"))
        {
            if (Music.resource != scaryMusic)
            {
                Music.resource = scaryMusic;
                Music.volume = 0.75f;
                jump.pitch = 0.3f;
                vines.pitch = 0.5f;
                jump.volume = 0.7f;
                dash.pitch = 0.5f;
                death.pitch = 0.5f;
                skybox.SetActive(false);
                Music.Play();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vines"))
        {
            isClimbing = false;
            vines.Stop();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            death.Play();
            transform.position = spawnPos;
        }
    }
}