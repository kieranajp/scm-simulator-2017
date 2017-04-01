using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public PlayerMovement Movement;
    public PlayerPickup Pickup;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
        var isMoving = Movement.Velocity.magnitude != 0;

        if (isMoving) {
            if (Pickup.IsCarrying) {
                animator.Play("workerWalkHold");
            } else {
                animator.Play("workerWalk");
            }
        } else {
            if (Pickup.IsCarrying) {
                animator.Play("workerIdleHold");
            } else {
                animator.Play("workerIdle");
            }
        }
		
	}
}
