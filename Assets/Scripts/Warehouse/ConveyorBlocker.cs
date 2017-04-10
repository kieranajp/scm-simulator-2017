using Pickable;
using UnityEngine;

namespace Warehouse
{
    public class ConveyorBlocker : MonoBehaviour
    {
        private Conveyor _conveyorBelt;

        private void Start()
        {
            _conveyorBelt = FindObjectOfType<Conveyor>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var item = other.GetComponent<Item>();
            if (item && item.enabled)
            {
                other.GetComponent<Item>().FiveSecondRule = false;
                _conveyorBelt.IsBlocked = true;
                StartCoroutine(other.GetComponent<Item>().StartPulsating());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<Item>())
            {
                _conveyorBelt.IsBlocked = false;
            }
        }
    }
}