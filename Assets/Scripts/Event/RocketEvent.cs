using UnityEngine;

public class RocketEvent : RandomEvent {

	public float Extension = 5.0f;

	public override void Fire() {
		var timer = GameObject.FindObjectOfType<Timer> ();

        FindObjectOfType<Floor>().GoCrazy(3, 1);

		timer.AddTime (Extension);
	}
}
