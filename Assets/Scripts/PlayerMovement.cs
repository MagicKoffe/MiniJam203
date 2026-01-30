using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    Vector2 moveDirection;

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
    }

    private void movePlayer()
    {
        playerRB.AddForce(moveDirection * speed);
    }

    private void getMovementDirection()
    {
        float HorizontalAxis = Input.GetAxisRaw("Horizontal");
        moveDirection.x = HorizontalAxis;

    }
}
