using System;
using UnityEngine;

[RequireComponent(typeof(Pickable))]
[RequireComponent(typeof(SpriteRenderer))]
public class GlowDecorator : ProximityDecorator {

    private Pickable pickable;
    private SpriteRenderer spriteRenderer;
    private bool _isGlowing;

    private void Start()
    {
        pickable = GetComponent<Pickable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (pickable.IsPickedUp) {
            Normalize();
            return;
        }

        if (inProximity) {
            Glow();
        }

        if (_isGlowing)
        {
            var alpha = 0.75f + Mathf.Sin(Time.time * 5) / 4;
            Debug.Log("Glowing " + alpha);
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
    }

    private void Normalize()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    private void Glow()
    {
        _isGlowing = true;
    }

    private void Hide()
    {
        _isGlowing = false;
    }

    public override void InProximity(GameObject gameObject)
    {
        Glow();
    }

    public override void OutOfProximity(GameObject gameObject)
    {
        Hide();
    }
}
