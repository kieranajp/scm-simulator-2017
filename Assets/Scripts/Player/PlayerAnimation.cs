using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public PlayerMovement Movement;
    public PlayerPickup Pickup;
    private Animator animator;
    private PlayerBehaviour player;

    private void Start()
    {
        player = GetComponent<PlayerBehaviour>();
        animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
        var isMoving = Movement.Velocity.magnitude != 0;
        var p = "player_" + player.Player.ToString().Substring(1, 1) + "_";

        if (isMoving) {
            if (Pickup.IsCarrying) {
                animator.Play(p + "WalkHold");
            } else {
                animator.Play(p + "Walk");
            }
        } else {
            if (Pickup.IsCarrying) {
                animator.Play(p + "IdleHold");
            } else {
                animator.Play(p + "Idle");
            }
        }
	}
}
