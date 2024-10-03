using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variable de déplacement")]
    public float speed = 1f; 
    public float jumpForce = 1f;

    private Rigidbody2D rb;
    private bool isGrounded = true; // Pour savoir si il touche le sol

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Sol"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Sol"))
        {
            isGrounded = false;
        }
    }
}
