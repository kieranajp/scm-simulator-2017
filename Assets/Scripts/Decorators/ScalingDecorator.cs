using System;
using UnityEngine;

[RequireComponent(typeof(Pickable))]
[RequireComponent(typeof(BoxCollider2D))]
public class ScalingDecorator : ProximityDecorator {

    private Pickable pickable;
    private bool _isScaling;
    public float PulsatingSpeed = 5f;
    public float Amplitude = 1f;
    public float BasicScale = 1;

    private Vector2 _colliderOffset;
    private Vector2 _colliderSize;
    private BoxCollider2D _collider2d;

    private void Start()
    {
        pickable = GetComponent<Pickable>();
        _collider2d = GetComponent<BoxCollider2D>();
        _colliderOffset = _collider2d.offset;
        _colliderSize = _collider2d.size;
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

        if (_isScaling)
        {
            var alpha = BasicScale + Mathf.Sin(Time.time * PulsatingSpeed) / Amplitude;
            transform.localScale = Vector2.one * alpha;

            var reverse = 1 / alpha;

            _collider2d.offset = _colliderOffset * reverse;
            _collider2d.size = _colliderSize * reverse;
        }
    }

    private void Normalize()
    {
        transform.localScale = Vector2.one;
    }

    private void Glow()
    {
        _isScaling = true;
    }

    private void Hide()
    {
        _isScaling = false;
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
