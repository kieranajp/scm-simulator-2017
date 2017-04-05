using UnityEngine;

namespace Decorators
{
    [RequireComponent(typeof(Pickable))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class GlowDecorator : ProximityDecorator {

        private Pickable _pickable;
        private SpriteRenderer _spriteRenderer;
        private bool _isGlowing;

        protected override void Start()
        {
            _pickable = GetComponent<Pickable>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_pickable.IsPickedUp) {
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
                _spriteRenderer.color = new Color(1, 1, 1, alpha);
            }
        }

        private void Normalize()
        {
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }

        private void Glow()
        {
            _isGlowing = true;
        }

        private void Hide()
        {
            _isGlowing = false;
        }

        public override void InProximity(GameObject go)
        {
            Glow();
        }

        public override void OutOfProximity(GameObject go)
        {
            Hide();
        }
    }
}
