using System;
using System.Collections;
using UnityEngine;

public class TruckMovement : MonoBehaviour {

    public Transform wheel;
    public Transform lorry;
    public Transform door;

    private float _curSeconds = 0;
    private bool _doorOpening = true;
    private bool _isFinishedRolling = false;
    Vector3 targetPosition = new Vector3(0, 0, 0);

	void Start () {
        StartCoroutine(MoveToBack());
        StartCoroutine(OpenDoor());
	}

	void OnTimerExpire () {
        _curSeconds = 0;
        StartCoroutine(CloseDoor());
        StartCoroutine(Deliver());
	}

    private void Update()
    {
        if (_isFinishedRolling)
        {
            lorry.transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * 20) / 100);
            door.transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * 20) / 100);
        }
    }

    private IEnumerator MoveToBack()
    {
        while (_curSeconds < 3)
        {
            yield return null;
            lorry.transform.localPosition = Vector3.Lerp(lorry.transform.localPosition, targetPosition, Time.unscaledDeltaTime * 2);
            wheel.transform.localPosition = Vector3.Lerp(wheel.transform.localPosition, targetPosition, Time.unscaledDeltaTime * 2);
            _curSeconds += Time.unscaledDeltaTime;
        }

        _isFinishedRolling = true;
    }

    private IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(1);
        while (_doorOpening)
        {
            door.transform.localScale = new Vector3(door.transform.localScale.x + Time.unscaledDeltaTime, 1, 1);
            if (door.transform.localScale.x > 0.95f)
            {
                _doorOpening = false;
            }

            yield return null;
        }
    }

    private IEnumerator Deliver()
    {
        yield return new WaitForSeconds(1);
        while (_curSeconds < 3)
        {
            yield return null;
            lorry.transform.localPosition += new Vector3(-Time.unscaledDeltaTime, 0, 0);
            wheel.transform.localPosition += new Vector3(-Time.unscaledDeltaTime, 0, 0);
            _curSeconds += Time.unscaledDeltaTime;
        }
    }

    private IEnumerator CloseDoor()
    {
        while (door.transform.localScale.x > 0.05f)
        {
            door.transform.localScale = new Vector3(door.transform.localScale.x - Time.unscaledDeltaTime, 1, 1);
            yield return null;
        }
        door.transform.localScale = Vector3.zero;
        _isFinishedRolling = false;
    }

}
