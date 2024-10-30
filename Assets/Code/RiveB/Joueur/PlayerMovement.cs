using Assets.Code.RiveB.World;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variable de déplacement")]
    public float speed = 1f;
    public float jumpForce = 1f;
    public float wallJumpForce = 1f;
    public float wallSlideSpeed = 0.5f;

    [Header("Autres..")]
    public LayerMask groundLayer;

    private Transform spawnPoint;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isTouchingWall = false;
    private int wallDirection = 0;
    private bool canJumpWall = true;

    public bool canMove = true;

    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Player Spawn").transform;
        transform.position = spawnPoint.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
        {
            CheckCollisions();

            float moveInput = Input.GetAxis("Horizontal");

            if (!isTouchingWall)
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }

            if (isGrounded && !canJumpWall && !isTouchingWall)
            {
                canJumpWall = true;
            }

            if (Input.GetButton("Jump"))
            {
                if (isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
                else if (isTouchingWall && !isGrounded)
                {
                    if ((wallDirection == -1 && moveInput > 0) || (wallDirection == 1 && moveInput < 0))
                    {
                        rb.velocity = new Vector2(wallJumpForce * -wallDirection, jumpForce);
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                        StartCoroutine(WaitAndExecute());
                        if (isTouchingWall)
                        {
                            canJumpWall = false;
                        }
                    }
                }
            }

            if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }

            if (Input.GetButtonDown("ReSpawn"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (!isTouchingWall && !isGrounded)
            {
                if (transform.position.y < -40)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

    IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(1f);
    }

    void CheckCollisions()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);

        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, groundLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, groundLayer);

        isTouchingWall = hitRight || hitLeft;
        wallDirection = hitRight ? 1 : hitLeft ? -1 : 0;
    }
}
