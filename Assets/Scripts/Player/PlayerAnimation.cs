using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        public PlayerMovement Movement;
        public PlayerPickup Pickup;
        private Animator _animator;
        private string _animationPrefix;

        private void Start()
        {
            var player = GetComponent<PlayerBehaviour>().Player;
            _animator = GetComponent<Animator>();
            var characterNumber = Game.PlayerCharacters[player];
            _animationPrefix = "player_" + characterNumber + "_";
        }

        private void Update()
        {
            var isMoving = Math.Abs(Movement.Velocity.magnitude) > 0.05f;

            if (isMoving)
            {
                if (Pickup.IsCarrying)
                {
                    _animator.Play(_animationPrefix + "WalkHold");
                }
                else
                {
                    _animator.Play(_animationPrefix + "Walk");
                }
            }
            else
            {
                if (Pickup.IsCarrying)
                {
                    _animator.Play(_animationPrefix + "IdleHold");
                }
                else
                {
                    _animator.Play(_animationPrefix + "Idle");
                }
            }
        }
    }
}