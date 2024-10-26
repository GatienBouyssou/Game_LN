using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variable de déplacement")]
    public float speed = 1f;
    public float jumpForce = 1f;
    public float wallSlideSpeed = 0.5f;

    [Header("Autres..")]
    public LayerMask groundLayer;

    private Transform spawnPoint;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isTouchingWall = false;

    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Player Spawn").transform;
        transform.position = spawnPoint.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckCollisions();

        if (!isTouchingWall)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || (!isGrounded && isTouchingWall)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed); 
        }

        if (Input.GetButtonDown("ReSpawn"))
        {
            transform.position = spawnPoint.position;
        }
    }

    void CheckCollisions()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, groundLayer); // Est ce qu'il vaudrait pas mieux faire un onColliderEnter2D ??

        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, groundLayer) ||
                         Physics2D.Raycast(transform.position, Vector2.left, 0.6f, groundLayer);
    }
}
