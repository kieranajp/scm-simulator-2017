using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : Actionable {
    public override void DoAction(Collider2D collision)
    {
        var pickup = collision.GetComponent<PlayerPickup>();
        if (pickup != null)
        {
            if (pickup.PickedUpObject.GetComponent<Box>() != null)
            {
                pickup.PutDownObject();
            }
        }
    }
}
