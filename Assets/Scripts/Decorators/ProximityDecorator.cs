using UnityEngine;

[RequireComponent(typeof(Collider2D))]
abstract public class ProximityDecorator : MonoBehaviour {

    public GameObject TriggerBody;
    protected bool inProximity = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rigidbody2d = collision.gameObject;
        if (TriggerBody == rigidbody2d)
        {
            InProximity(TriggerBody);
            inProximity = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var rigidbody2d = collision.gameObject;
        if (TriggerBody == rigidbody2d)
        {
            OutOfProximity(TriggerBody);
            inProximity = false;
        }
    }

    abstract public void InProximity(GameObject gameObject);
    abstract public void OutOfProximity(GameObject gameObject);
}
