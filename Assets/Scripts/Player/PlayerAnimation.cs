using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public PlayerMovement Movement;
    public PlayerPickup Pickup;
    private Animator _animator;
    private PlayerBehaviour _player;

    private void Start()
    {
        _player = GetComponent<PlayerBehaviour>();
        _animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
        var isMoving = Math.Abs(Movement.Velocity.magnitude) > 0.05f;
        var p = "player_" + (int)_player.Player + "_";

        if (isMoving) {
            if (Pickup.IsCarrying) {
                _animator.Play(p + "WalkHold");
            } else {
                _animator.Play(p + "Walk");
            }
        } else {
            if (Pickup.IsCarrying) {
                _animator.Play(p + "IdleHold");
            } else {
                _animator.Play(p + "Idle");
            }
        }
	}
}
