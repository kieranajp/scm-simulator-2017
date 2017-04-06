using Player;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerChoose
{
    public class SelectScreen : MonoBehaviour
    {
        public GameObject Arrow;
        public Color[] PlayerColors;

        private Game _game;
        private CharacterPool _characterPool;

        private void Start ()
        {
            _game = FindObjectOfType<Game>();
            _characterPool = FindObjectOfType<CharacterPool>();

            var players = _game.NumberOfPlayers;
            for (var i = 1; i <= players; i++)
            {
                InstantiateArrows(i);
            }
        }

        private void InstantiateArrows(int playerNumber)
        {
            var parent = _characterPool.CharacterInstances[playerNumber - 1].transform;
            var arrow = Instantiate(Arrow, transform);

            var arrowWidth = arrow.GetComponent<RectTransform>().sizeDelta.x;
            var x = arrowWidth * (playerNumber - 1);

            arrow.name = "player_" + playerNumber + "_arrow";
            arrow.transform.SetParent(parent);
            arrow.GetComponentInChildren<Text>().text = playerNumber.ToString();
            arrow.GetComponentInChildren<Image>().color = PlayerColors[playerNumber - 1];
            arrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
            arrow.GetComponent<CharacterArrow>().CurrentCharacter = playerNumber - 1;
            arrow.GetComponent<CharacterArrow>().PlayerController = playerNumber.ToPlayer();
        }
    }
}
