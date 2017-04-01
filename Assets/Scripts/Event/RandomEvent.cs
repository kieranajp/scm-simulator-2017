using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RandomEvent : MonoBehaviour {

	public abstract string Name {
		get;
	}

	public abstract string Avatar {
		get;
	}

	public abstract string Message {
		get;
	}

	public abstract void Fire ();
}
