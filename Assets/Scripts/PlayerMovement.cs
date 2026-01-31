using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private float castDistance;
    [SerializeField] private Vector2 playerSize;
    [SerializeField] private LayerMask groundLayer;

    Vector2 moveDirection;

    private bool justJumped = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        getMovementDirection();
        checkIfOnGround();
    }

    private bool checkIfOnGround()
    {
        if(Physics2D.BoxCast(transform.position, playerSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, playerSize);
    }

    private void movePlayer()
    {
        playerRB.AddForce(moveDirection * speed);

        if (justJumped)
        {
            justJumped = false;
            playerRB.AddForce(Vector2.up * jumpForce * 100);
        }
    }

    private void getMovementDirection()
    {
        float HorizontalAxis = Input.GetAxisRaw("Horizontal");
        moveDirection.x = HorizontalAxis;

        if(Input.GetKeyDown(KeyCode.Space) && !justJumped && checkIfOnGround())
        {
            justJumped = true;
        }

    }
}
