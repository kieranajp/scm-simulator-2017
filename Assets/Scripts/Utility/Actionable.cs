using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlayerBehaviour))]
abstract public class Actionable : MonoBehaviour {

    public bool RequiresAction;
    private List<Collider2D> _colliders = new List<Collider2D>();

    private void Update()
    {
        if (RequiresAction)
        {
            foreach (var c in _colliders)
            {
                DoAction(c);
            }
        }
    }

    public abstract void DoAction(Collider2D collision);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _colliders = new List<Collider2D>(collision.GetComponents<Collider2D>());

        if (!RequiresAction)
        {
            DoAction(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _colliders = new List<Collider2D>();
    }
}
