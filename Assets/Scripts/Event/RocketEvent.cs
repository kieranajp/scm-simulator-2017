using UnityEngine;

public class RocketEvent : RandomEvent {

	public float Extension = 5.0f;

	public override void Fire() {
		var timer = GameObject.FindObjectOfType<Timer> ();

		timer.AddTime (Extension);
	}
}
