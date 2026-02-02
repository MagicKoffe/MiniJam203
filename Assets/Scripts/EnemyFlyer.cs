using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyer : MonoBehaviour
{
    private GameManager gm;
    private Transform player;
    private SpriteRenderer enemySR;
    private Rigidbody2D enemyRB;
    [SerializeField] float speed;
    public ColourManager enemyCM;
    public Color currentColour;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyRB = GetComponent<Rigidbody2D>();
        enemyCM = GetComponent<ColourManager>();
        enemySR = GetComponentInChildren<SpriteRenderer>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (enemyCM.currentColour == 1)
        {
            currentColour = Color.blue;
        }
        else
        {
            currentColour = Color.red;
        }

        enemySR.color = currentColour;
    }

    public void killed()
    {
        gm.addkillScore();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        moveTowardsPlayer();
    }

    private void moveTowardsPlayer()
    {
        enemyRB.AddForce(transform.up * speed);
    }

    // Update is called once per frame
    void Update()
    {
        RotateToPlayer();
    }

    private void RotateToPlayer()
    {
        Vector3 look = transform.InverseTransformPoint(player.position);
        float Angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;

        transform.Rotate(0, 0, Angle);
    }
}
