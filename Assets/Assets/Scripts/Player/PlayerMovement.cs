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

    // Update is called once per frame
    void Update () {
        var horizontal = PlayerInput.GetAxis("Horizontal", Player) * Time.deltaTime * Speed;
        var vertical = PlayerInput.GetAxis("Vertical", Player) * Time.deltaTime * Speed;

        transform.Translate(horizontal, vertical, 0);
    }
}
