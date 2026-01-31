using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    public Transform reticle;
    public Transform compass;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;

    [SerializeField] private float castDistance;
    [SerializeField] private Vector2 playerSize;
    [SerializeField] private LayerMask groundLayer;

    Vector2 mouseDirection;
    bool disableMove;
    Vector2 moveDirection;
    private bool hasUsedAirJump = false;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (disableMove)
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
        disableMove = true;
        playerRB.AddForce(mouseDirection * dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        disableMove = false;
    }

    private void Jump()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}