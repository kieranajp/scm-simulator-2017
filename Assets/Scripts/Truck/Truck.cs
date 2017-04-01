using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : Actionable {
    public AudioClip ThrowSound;

    private void Update()
    {
        transform.localPosition += new Vector3(0, Mathf.Sin(Time.unscaledTime * 20) / 200);
    }

    public override void DoAction(Collider2D collision)
    {
        var pickup = collision.GetComponent<PlayerPickup>();
        if (pickup != null)
        {
            var pickedUpObject = pickup.PickedUpObject;
            if (pickedUpObject && pickedUpObject.GetComponent<Box>() != null)
            {
                var box = pickedUpObject.GetComponent<Box>();
                pickup.PutDownObject();
                box.ThrowBox();
                AudioSource.PlayClipAtPoint(ThrowSound, FindObjectOfType<AudioSource>().transform.position);
            }
        }
    }
}
