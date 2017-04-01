using UnityEngine;

public class Pickable : MonoBehaviour
{
    public bool IsPickedUp;
    public bool CanBePicked = true;

    public bool FiveSecondRule = true;
    public float MaxTimeOnFloor = 5f;
    Vector3 lastPosition = Vector3.zero;
    float timeOnFloor = 0;

    virtual public void PickUp()
    {
        IsPickedUp = true;
    }

    virtual public void PutDown()
    {
        IsPickedUp = false;
    }

    void FixedUpdate()
    {
        float speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;

        if (speed < 0.01f && !IsPickedUp && FiveSecondRule)
        {
            timeOnFloor += Time.fixedDeltaTime;
        }
        else {
            timeOnFloor = 0;
        }

        if(timeOnFloor > MaxTimeOnFloor)
        {
            Destroy(gameObject);
        }
    }
}
