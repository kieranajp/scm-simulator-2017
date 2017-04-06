using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Sprite ArrowSprite;
    public int NumberOfPlayers;

    public static Dictionary<Player.Player, int> PlayerCharacters = new Dictionary<Player.Player, int>
    {
        {Player.Player.P1, 1},
        {Player.Player.P2, 2},
        {Player.Player.P3, 3},
        {Player.Player.P4, 4}
    };

    private void Start ()
    {
        NumberOfPlayers = Input.GetJoystickNames().Length;
    }

    public static void SetCharacter(Player.Player player, int character)
    {
        PlayerCharacters[player] = character;
    }
}
