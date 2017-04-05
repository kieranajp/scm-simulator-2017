using UnityEngine;
using UnityEngine.UI;

public class Floor : MonoBehaviour {

    public RawImage FloorTexture;

    public float Duration = 10;
    public float FlashDuration;

    private float _speed = 1;
    private Color _basicColor;

    private void Start () {
        _basicColor = FloorTexture.color;
	}
	
	// Update is called once per frame
    private void Update () {
		if(FlashDuration >= 0)
        {
            var r = Mathf.PingPong(Time.time * (_speed + 1), 1);
            var g = Mathf.PingPong(Time.time * (_speed + 2), 1);
            var b = Mathf.PingPong(Time.time * (_speed + 3), 1);
            FloorTexture.color = new Color(r, g, b);
            FlashDuration -= Time.deltaTime;
        }
        else
        {
            FloorTexture.color = _basicColor;
        }
	}

    public void GoCrazy (float duration, float s)
    {
        _speed = s;
        FlashDuration = duration;
    }
}
