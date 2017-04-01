using UnityEngine;

[RequireComponent(typeof(Pickable))]
public class ArrowDecorator : ProximityDecorator {

    public GameObject Arrow;
    public Vector3 Offset = new Vector3(0, 1f);
    private Pickable pickable;

    private void Start()
    {
        pickable = GetComponent<Pickable>();
    }

    private void Update()
    {
        if (pickable.IsPickedUp) {
            Hide();
            return;
        }

        if (inProximity) { 
            Arrow.SetActive(true);
            Arrow.transform.position = transform.position + Offset;
            var scaledValue = 1 + Mathf.PingPong(Time.time * 3, 1);

            Arrow.transform.localScale = new Vector3(scaledValue, scaledValue);
        }
    }

    private void Hide()
    {
        Arrow.SetActive(false);
    }

    public override void InProximity(GameObject gameObject)
    {
        Arrow.SetActive(true);
    }

    public override void OutOfProximity(GameObject gameObject)
    {
        Arrow.SetActive(false);
    }
}
