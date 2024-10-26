using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovements : MonoBehaviour
{
    public GameObject classicMissile;
    public float speed = 5f;
    public float dashSpeed = 50f;
    public float dashTime = 0.3f;
    public float dashCooldown = 3f;
    public float bounceForce = 1f;
    public TMP_Text cooldownText;

    private Rigidbody2D rb;
    private float dashTimeLeft;
    private float nextDash;
    private bool isDashing;
    private bool dashInCooldown;
    private bool playerCollision = true;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextDash = Time.time;
    }

    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            FireMissile();
        }

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontalMove, verticalMove);

        if (!isDashing)
        {
            rb.velocity = speed * movement;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && !dashInCooldown)
        {
            StartCoroutine(Dash(movement));
        }

        if (!isDashing && dashInCooldown)
        {
            float remainingTime = nextDash - Time.time;
            if (remainingTime <= 0)
            {
                dashInCooldown = false;
                cooldownText.text = "Ready !";
            }
            else
            {
                cooldownText.text = remainingTime.ToString("0.0");
            }
        }
    }

    IEnumerator Dash(Vector2 direction)
    {
        GetComponent<Collider2D>().enabled = false;
        isDashing = true;
        dashTimeLeft = dashTime; // Reset the dash timer.

        while (dashTimeLeft > 0f)
        {
            dashTimeLeft -= Time.deltaTime; // Lower the dash timer each frame.

            rb.velocity = direction * dashSpeed; // Dash in the direction that was held down.
            // No need to multiply by Time.DeltaTime here, physics are already consistent across different FPS.

            yield return null; // Returns out of the coroutine this frame so we don't hit an infinite loop.
        }

        rb.velocity = new Vector2(0f, 0f); // Stop dashing.

        isDashing = false;
        GetComponent<Collider2D>().enabled = true;
        dashInCooldown = true;
        nextDash = Time.time + dashCooldown;
    }

    void FireMissile()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        Debug.Log(direction);
        direction.Scale(transform.localScale+new Vector3(0.1f,0.1f,0.1f));
        direction.Scale(new Vector3(10,10,0));
        Debug.Log(direction);
        Vector3 initialMissilePos = transform.position+direction;
        GameObject missile = Instantiate(classicMissile, initialMissilePos, Quaternion.LookRotation(Vector3.forward, direction));
    }

    void BounceOffObstacle(Collision2D collision)
    {
        // Calculate the direction to bounce
        Vector2 bounceDirection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);
        rb.velocity = bounceDirection * bounceForce;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!playerCollision) return;
    }
}