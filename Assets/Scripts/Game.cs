using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public int NumberOfPlayers;
    private static GameObject _self;

    public static Dictionary<Player.Player, int> PlayerCharacters = new Dictionary<Player.Player, int>
    {
        {Player.Player.P1, 1},
        {Player.Player.P2, 2},
        {Player.Player.P3, 3},
        {Player.Player.P4, 4}
    };


    private void Awake()
    {
        if (_self == null)
        {
            _self = gameObject;
            DontDestroyOnLoad(_self);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Start()
    {
        NumberOfPlayers = Input.GetJoystickNames().Length;
    }

    public static void SetCharacter(Player.Player player, int character)
    {
        var newCharacterPlayer = PlayerCharacters.FirstOrDefault(x => x.Value == character).Key;
        var oldCharacter = PlayerCharacters[player];
        PlayerCharacters[player] = character;
        PlayerCharacters[newCharacterPlayer] = oldCharacter;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu");
        }
    }
}