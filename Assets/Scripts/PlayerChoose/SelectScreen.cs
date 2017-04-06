using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PlayerChoose
{
    public class SelectScreen : MonoBehaviour
    {
        public GameObject Arrow;
        public Color[] PlayerColors;
        public Image FadeOffImage;

        private Game _game;
        private CharacterPool _characterPool;
        private int _numberPlayersSelected;

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

        public void PlayerSelected()
        {
            _numberPlayersSelected++;
            if (_numberPlayersSelected == _game.NumberOfPlayers)
            {
                StartCoroutine(StartLevel());
            }
        }

        private IEnumerator StartLevel()
        {
            yield return FadeOffCharacters();
            yield return new WaitForSeconds(2f);
            yield return FadeOffScreen();
        }

        private IEnumerator FadeOffScreen()
        {
            while (FadeOffImage.color.a < 1)
            {
                var c = FadeOffImage.color;
                FadeOffImage.color = new Color(c.r, c.g, c.b, c.a + Time.deltaTime);
                yield return null;
            }

            SceneManager.LoadScene("Level_1");
        }

        private IEnumerator FadeOffCharacters()
        {
            var characters = FindObjectsOfType<Character>();
            List<Image> allImages = new List<Image>();
            foreach (var c in characters)
            {
                if (c.IsSelected)
                {
                    c.GetComponentInChildren<Animator>().enabled = true;
                    c.GetComponentInChildren<Image>().color = Color.white;
                    continue;
                }

                var images = c.GetComponentsInChildren<Image>();
                allImages.AddRange(images);
            }

            while (true)
            {
                var lastAlpha = 0f;
                foreach (var image in allImages)
                {
                    image.color = new Color(
                        image.color.a,
                        image.color.g,
                        image.color.b,
                        image.color.a - Time.unscaledDeltaTime
                    );

                    lastAlpha = image.color.a;
                }
                if (lastAlpha <= 0)
                {
                    break;
                }
                yield return null;
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
