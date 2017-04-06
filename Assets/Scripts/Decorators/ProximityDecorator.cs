using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
abstract public class ProximityDecorator : MonoBehaviour {

    public List<GameObject> TriggerBodies = new List<GameObject>();
    protected bool inProximity = false;

    virtual protected void Start()
    {
        if (TriggerBodies.Count == 0)
        {
            var playerMovements = FindObjectsOfType<PlayerMovement>();
            foreach (var p in playerMovements)
            {
                TriggerBodies.Add(p.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var go = collision.gameObject;
        if (TriggerBodies.Contains(go))
        {
            InProximity(go);
            inProximity = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var go = collision.gameObject;
        if (TriggerBodies.Contains(go))
        {
            OutOfProximity(go);
            inProximity = false;
        }
    }

    abstract public void InProximity(GameObject go);
    abstract public void OutOfProximity(GameObject go);
}
