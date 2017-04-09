using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class EventDispatcher : MonoBehaviour
    {
        public float FirstEvent = 10.0f;
        public int EventChance = 6;
        public float EventTick = 7.0f;

        public List<RandomEvent> EventTypes;

        public void Stop()
        {
            CancelInvoke();
        }

        private void Start()
        {
            InvokeRepeating("RollForEvent", FirstEvent, EventTick);
        }

        // Attempt to make an event occur
        private void RollForEvent()
        {
            var shouldEventOccur = Random.Range(1, EventChance);

            if (shouldEventOccur == 1)
            {
                var evt = ChooseEvent();

                evt.Fire();
                var listeners = GameObject.FindGameObjectsWithTag("EventListener");
                foreach (var listener in listeners)
                {
                    listener.BroadcastMessage("OnEventFired", evt);
                }
            }
        }

        private RandomEvent ChooseEvent()
        {
            return EventTypes[Random.Range(0, EventTypes.Count)];
        }
    }
}