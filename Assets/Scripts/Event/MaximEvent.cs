using System.Collections;
using Player;
using UnityEngine;

namespace Event
{
    public class MaximEvent : RandomEvent
    {
        public float Boost = 3.0f;
        public float Duration = 5.0f;


        public override void Fire()
        {
            SpeedUp();
            StartCoroutine(ResetSpeed());
        }

        private void SpeedUp()
        {
            var players = FindObjectsOfType<PlayerMovement>();

            foreach (var player in players)
            {
                player.GetComponent<SpriteRenderer>().color = Color.red;
                player.Speed += Boost;
            }
            ResetCollision(true);
        }

        private IEnumerator ResetSpeed()
        {
            yield return new WaitForSeconds(Duration);

            var players = FindObjectsOfType<PlayerMovement>();

            foreach (var player in players)
            {
                player.GetComponent<SpriteRenderer>().color = Color.white;
                player.Speed = player.OriginalSpeed;
            }
            ResetCollision(false);
        }

        private void ResetCollision(bool ignore)
        {
            var players = FindObjectsOfType<PlayerMovement>();

            foreach (var player1 in players)
            {
                foreach (var player2 in players)
                {
                    Physics2D.IgnoreCollision(
                        player1.GetComponent<Collider2D>(),
                        player2.GetComponent<Collider2D>(),
                        ignore
                    );
                }
            }
        }
    }
}