using System.Collections;
using Player;
using UnityEngine;

namespace Event
{
    public class WarnarEvent : RandomEvent
    {
        public float Duration = 3.0f;
        public float Penalty = -3.0f;

        public override void Fire()
        {
            GetComponent<AudioSource>().Play();

            SlowDown();
            StartCoroutine(ResetSpeed());
        }

        private void SlowDown()
        {
            var players = FindObjectsOfType<PlayerMovement>();

            foreach (var player in players)
            {
                player.GetComponent<SpriteRenderer>().color = Color.blue;
                player.Speed += Penalty;
            }
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
        }
    }
}