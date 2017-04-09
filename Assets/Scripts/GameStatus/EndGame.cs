using System.Collections;
using Box;
using Event;
using JetBrains.Annotations;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameStatus
{
    public class EndGame : MonoBehaviour {

        private bool _isFinished = false;
        public AudioClip GameOver;
        public float AnimationSpeed = 0.2f;
        public GameObject[] AnimateObjects;
        private EventDispatcher _eventDispatcher;
        public Text[] GoodBoxes;
        public Text[] BadBoxes;
        public Text TotalGoodBoxes;
        public Text TotalBadBoxes;
        public Image Status;
        public Sprite WinningSprite;
        public Sprite LostSprite;
        private bool _canSkip;

        private void Start()
        {
            _eventDispatcher = FindObjectOfType<EventDispatcher>();
        }

        private void Update()
        {
            if (_canSkip && Input.anyKey)
            {
                SceneManager.LoadScene("Level_1");
            }
        }

        public void Finish()
        {
            if (!_isFinished)
            {
                FindObjectOfType<Camera>().GetComponent<AudioSource>().clip = GameOver;
                FindObjectOfType<Camera>().GetComponent<AudioSource>().loop = false;
                FindObjectOfType<Camera>().GetComponent<AudioSource>().Play();
                UpdateScores();
                FreezePlayers();
                StartCoroutine(AnimateBoard());
            }
            _isFinished = true;
        }

        [UsedImplicitly]
        private void OnTimerExpire()
        {
            Finish();
        }

        private void UpdateScores()
        {
            var players = FindObjectsOfType<PlayerBehaviour>();
            var scores = FindObjectOfType<BoxManager>();

            foreach (var p in players)
            {
                var pIndex = (int) p.Player - 1;
                GoodBoxes[pIndex].text = "x " + scores.PlayerGoods[pIndex];
                BadBoxes[pIndex].text = "x " + scores.PlayerWrongs[pIndex];
            }

            TotalGoodBoxes.text = "x " + scores.TotalGood;
            TotalBadBoxes.text = "x " + scores.TotalBad;

            if (scores.TotalGood >= 3) {
                Status.sprite = WinningSprite;
            } else {
                Status.color = Color.red;
                Status.sprite = LostSprite;
            }
        }

        private void FreezePlayers()
        {
            _eventDispatcher.Stop();
            _eventDispatcher.enabled = false;
            var players = FindObjectsOfType<PlayerMovement>();
            foreach(var p in players)
            {
                p.Stop();
                Destroy(p);
            }
        }

        private IEnumerator AnimateBoard()
        {
            foreach (var obj in AnimateObjects)
            {
                yield return new WaitForSeconds(AnimationSpeed);
                obj.SetActive(true);
            }

            _canSkip = true;
        }
    }
}
