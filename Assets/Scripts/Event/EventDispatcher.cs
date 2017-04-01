using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher : MonoBehaviour {

	public float FirstEvent  = 10.0f;
	public int   EventChance = 6;
	public float EventTick   = 10.0f;

	protected List<RandomEvent> EventTypes;

	// Use this for initialization
	void Start () {
		EventTypes = new List<RandomEvent> ();

		EventTypes.Add (new WarnarEvent ());
	}
	
	// Update is called once per frame
	void Update () {
		InvokeRepeating ("RollForEvent", FirstEvent, EventTick);
	}

	// Attempt to make an event occur
	private void RollForEvent() {
		var ShouldEventOccur = Random.Range (1, EventChance);

		if (ShouldEventOccur == 1) {
			var evt = ChooseEvent ();

			evt.Fire ();
		}
	}

	private RandomEvent ChooseEvent() {
		return EventTypes [Random.Range (0, EventTypes.Count - 1)];
	}
}
