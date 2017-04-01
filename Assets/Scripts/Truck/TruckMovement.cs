using System.Collections;
using UnityEngine;

public class TruckMovement : MonoBehaviour {

    public Transform door;

    private float _curSeconds = 0;
    Vector3 targetPosition = new Vector3(0, 0, 0);

	void Start () {
        transform.localPosition = new Vector3(-3, 0, 0);
        StartCoroutine(MoveToBack());
	}

    private void Update()
    {
        transform.localPosition += new Vector3(0, Mathf.Sin(Time.unscaledTime * 20) / 200);
    }

    private IEnumerator MoveToBack()
    {
        while (_curSeconds < 4)
        {
            yield return null;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.unscaledDeltaTime);
            _curSeconds += Time.unscaledDeltaTime;
        }
    }
}
