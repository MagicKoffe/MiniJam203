using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyer : MonoBehaviour
{
    private Transform player;
    [SerializeField] float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
