using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnarEvent : RandomEvent {

	public override string Name {
		get {
			return "Warnar";
		}
	}

	public override string Avatar {
		get {
			return "warnar.jpg";
		}
	}

	public override string Message {
		get {
			return string.Format ("{0} spilled his drink!", Name);
		}
	}

	public override void Fire() {
		Debug.Log (string.Format("Firing {0}!", Name));
	}
}
