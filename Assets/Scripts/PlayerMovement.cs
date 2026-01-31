using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private ColourManager playerCM;
    public Transform reticle;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;

    [SerializeField] private float castDistance;
    [SerializeField] private Vector2 playerSize;
    [SerializeField] private LayerMask groundLayer;

    Vector2 mouseDirection;
    bool isDashing = false;
    Vector2 moveDirection;
    private bool hasUsedAirJump = false;

    void Start()
    {
        Cursor.visible = false;

        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCM = GetComponent<ColourManager>();
    }

    private void FixedUpdate()
    {
        if (isDashing)
            return;

        movePlayer();
    }

    void Update()
    {
        getMovementDirection();
        getMousePosition();
    }

    private void getMousePosition()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        reticle.position = worldPosition;

        mouseDirection = worldPosition - transform.position;
        mouseDirection.Normalize();
    }

    private bool checkIfOnGround()
    {
        return Physics2D.BoxCast(transform.position, playerSize, 0, -transform.up, castDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, playerSize);
    }

    private void movePlayer()
    {
        playerRB.AddForce(moveDirection * speed);

        if(playerRB.velocity.magnitude > maxSpeed)
        {
            playerRB.velocity = Vector2.ClampMagnitude(playerRB.velocity, maxSpeed);
        }
    }

    private void getMovementDirection()
    {
        float HorizontalAxis = Input.GetAxisRaw("Horizontal");
        moveDirection.x = HorizontalAxis;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (checkIfOnGround())
            {
                // Ground jump - reset air jump availability
                Jump();
                hasUsedAirJump = false;
            }
            else if (!hasUsedAirJump)
            {
                // Air jump - can only be used once until landing
                Jump();
                hasUsedAirJump = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        playerAnimator.SetTrigger("Dash");
        playerRB.AddForce(mouseDirection * dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isDashing = false;
    }

    private void Jump()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            ColourManager enemyCM = collision.transform.GetComponent<ColourManager>();

            if (playerCM.currentColour == enemyCM.currentColour)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}