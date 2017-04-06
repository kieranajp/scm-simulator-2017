using UnityEngine;

namespace Player
{
    public enum Player
    {
        P1 = 1,
        P2 = 2,
        P3 = 3,
        P4 = 4
    }

    internal static class PlayerControllerExtender
    {
        public static Player ToPlayer(this int p)
        {
            switch (p)
            {
                case 1: return Player.P1;
                case 2: return Player.P2;
                case 3: return Player.P3;
                case 4: return Player.P4;
            }
            return Player.P1;
        }
    }

    public class PlayerBehaviour : MonoBehaviour {
        public Player Player;
    }
}