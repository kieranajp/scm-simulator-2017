using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

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

    private void OnTimerExpire()
    {
        FindObjectOfType<Camera>().GetComponent<AudioSource>().clip = GameOver;
        FindObjectOfType<Camera>().GetComponent<AudioSource>().loop = false;
        FindObjectOfType<Camera>().GetComponent<AudioSource>().Play();
        UpdateScores();
        FreezePlayers();
        StartCoroutine(AnimateBoard());
    }

    private void UpdateScores()
    {
        var players = FindObjectsOfType<PlayerBehaviour>();
        var scores = FindObjectOfType<BoxManager>();

        foreach (var p in players)
        {
            var pIndex = (int) p.Player - 1;
            GoodBoxes[pIndex].text = "x " + scores.playerGoods[pIndex];
            BadBoxes[pIndex].text = "x " + scores.playerWrongs[pIndex];
        }

        TotalGoodBoxes.text = "x " + scores.totalGood;
        TotalBadBoxes.text = "x " + scores.totalBad;

        if (scores.totalGood >= 3) {
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
