using Assets.Code.RiveB.World;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variable de déplacement")]
    public float speed = 1f;
    public float jumpForce = 1f;
    public float wallJumpForce = 1f;
    public float wallSlideSpeed = 0.5f;

    [Header("Autres..")]
    public LayerMask groundLayer;
    public Tilemap fondationTilemap;

    private Transform spawnPoint;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isTouchingWall = false;
    private int wallDirection = 0;

    public bool canMove = true;
    private bool health = false;

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

            if (Input.GetButton("Jump"))
            {
                if (isGrounded | isOnPlatform())
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
                else if (isTouchingWall && !isGrounded)
                {
                    if ((wallDirection == -1 && moveInput > 0) || (wallDirection == 1 && moveInput < 0))
                    {
                        rb.velocity = new Vector2(wallJumpForce * -wallDirection, jumpForce);
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

    private bool isOnPlatform()
    {
        if (isGrounded || !isTouchingWall) return false;

        Vector3Int wallTilePosition = fondationTilemap.WorldToCell(transform.position + new Vector3(wallDirection * 0.6f, 0, 0));

        if (fondationTilemap.GetTile(wallTilePosition) == null) return false;

        Vector3Int tileAbove = wallTilePosition + Vector3Int.up;
        if (fondationTilemap.GetTile(tileAbove) == null) return true;

        Vector3Int tileTwoAbove = wallTilePosition + Vector3Int.up * 2;
        if (fondationTilemap.GetTile(tileTwoAbove) == null) return true;

        Vector3Int tileBelow = wallTilePosition + Vector3Int.down;
        if (fondationTilemap.GetTile(tileBelow) == null) return true;

        return false;
    }

    void CheckCollisions()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);

        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, groundLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, groundLayer);

        isTouchingWall = hitRight || hitLeft;
        wallDirection = hitRight ? 1 : hitLeft ? -1 : 0;
    }

    public bool isPlayerGrounded()
    {
        return isGrounded;
    }

    internal void RemoveLifeSavior()
    {
        health = false;
    }
    internal void AddLifeSavior()
    {
        health = true;
    }

    internal bool hasLifeSavior()
    {
        return health;
    }
}
