using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public AudioClip gameOver;
    public float AnimationSpeed = 0.2f;
    public GameObject[] AnimateObjects;
    private Score score;
    private EventDispatcher eventDispatcher;
    public Text[] goodBoxes;
    public Text[] badBoxes;
    public Text totalGoodBoxes;
    public Text totalBadBoxes;
    public Image status;
    public Sprite winningSprite;
    public Sprite lostSprite;
    private bool _canSkip = false;

    private void Start()
    {
        score = FindObjectOfType<Score>();
        eventDispatcher = FindObjectOfType<EventDispatcher>();
    }

    private void Update()
    {
        if (_canSkip && Input.anyKey)
        {
            SceneManager.LoadScene("Level_1");
        }
    }

    void OnTimerExpire()
    {
        FindObjectOfType<Camera>().GetComponent<AudioSource>().clip = gameOver;
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
            var pIndex = int.Parse(p.Player.ToString().Substring(1, 1)) - 1;
            goodBoxes[pIndex].text = "x " + scores.playerGoods[pIndex].ToString();
            badBoxes[pIndex].text = "x " + scores.playerWrongs[pIndex].ToString();
        }

        totalGoodBoxes.text = "x " + scores.totalGood;
        totalBadBoxes.text = "x " + scores.totalBad;

        if (scores.totalGood >= 3) {
            status.sprite = winningSprite;
        } else {
            status.color = Color.red;
            status.sprite = lostSprite;
        }
    }

    private void FreezePlayers()
    {
        eventDispatcher.Stop();
        eventDispatcher.enabled = false;
        var players = GameObject.FindObjectsOfType<PlayerMovement>();
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
