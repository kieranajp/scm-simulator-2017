using Player;
using UnityEngine;

namespace PlayerLevel
{
    public class CharacterSpawner : MonoBehaviour
    {
        public Transform[] SpawningPoints;
        public GameObject[] Characters;
        private int _numberOfPlayers;

        private void Start()
        {
            _numberOfPlayers = FindObjectOfType<Game>().NumberOfPlayers;
            for (var i = 1; i <= _numberOfPlayers; i++)
            {
                var playerCharacter = Game.PlayerCharacters[i.ToPlayer()];
                InstantiateCharacter(Characters[playerCharacter - 1], SpawningPoints[i - 1].position, i.ToPlayer());
            }
        }

        private void InstantiateCharacter(GameObject go, Vector3 spawningPoint, Player.Player player)
        {
            var character = Instantiate(go, spawningPoint, Quaternion.identity);
            var playerBehaviour = character.GetComponent<PlayerBehaviour>();
            playerBehaviour.Player = player;
        }
    }
}