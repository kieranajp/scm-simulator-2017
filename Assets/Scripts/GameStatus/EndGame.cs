using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    public float AnimationSpeed = 0.2f;
    public GameObject[] AnimateObjects;
    private Score score;
    private EventDispatcher eventDispatcher;

    private void Start()
    {
        score = FindObjectOfType<Score>();
        eventDispatcher = FindObjectOfType<EventDispatcher>();
    }

    void OnTimerExpire()
    {
        FreezePlayers();
        StartCoroutine(AnimateBoard());
    }

    private void FreezePlayers()
    {
        eventDispatcher.enabled = false;
        var players = GameObject.FindObjectsOfType<PlayerMovement>();
        foreach(var p in players)
        {
            p.Speed = 0;
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
    }
}
