using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player
{
    P1,
    P2,
    P3,
    P4
};

public class PlayerMovement : MonoBehaviour {

    public Player Player;
    public float Speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        var horizontal = PlayerInput.GetAxis("Horizontal", Player) * Speed;
        var vertical = PlayerInput.GetAxis("Vertical", Player) * Speed;
        rb.velocity = new Vector3(horizontal, vertical, 0);
    }
}
