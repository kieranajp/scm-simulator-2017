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

        if (isMoving) {
            if (Pickup.IsCarrying) {
                animator.Play("workerWalkHold_" + player.Player.ToString().Substring(1, 1));
            } else {
                animator.Play("workerWalk_" + player.Player.ToString().Substring(1, 1));
            }
        } else {
            if (Pickup.IsCarrying) {
                animator.Play("workerIdleHold_" + player.Player.ToString().Substring(1, 1));
            } else {
                animator.Play("workerIdle_" + player.Player.ToString().Substring(1, 1));
            }
        }
		
	}
}
