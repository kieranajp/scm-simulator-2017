using Player;

namespace Event
{
    public class FedeEvent : RandomEvent
    {
        public override void Fire()
        {
            var players = FindObjectsOfType<PlayerMovement>();

            var newPosition = players[players.Length - 1].transform.position;

            foreach (var player in players)
            {
                var oldPosition = player.transform.position;

                player.transform.position = newPosition;

                newPosition = oldPosition;
            }
        }
    }
}