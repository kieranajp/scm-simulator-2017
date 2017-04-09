using System.Collections;
using Event;
using UnityEngine;

namespace Pickable
{
    public class Pickable : MonoBehaviour
    {
        public bool IsPickedUp;
        public bool CanBePicked = true;

        public bool FiveSecondRule = true;
        public float MaxTimeOnFloor = 5f;
        private Vector3 _lastPosition = Vector3.zero;
        private float _timeOnFloor;

        public virtual void PickUp()
        {
            IsPickedUp = true;
        }

        public virtual void PutDown()
        {
            IsPickedUp = false;
        }

        public void Explode()
        {
            var explosion = FindObjectOfType<AlvaroEvent>().Explosion;
            StartCoroutine(ExplodePickable(explosion));
        }

        private void FixedUpdate()
        {
            var speed = (transform.position - _lastPosition).magnitude;
            _lastPosition = transform.position;

            if (speed < 0.01f && !IsPickedUp && FiveSecondRule)
            {
                _timeOnFloor += Time.fixedDeltaTime;
            }
            else {
                _timeOnFloor = 0;
            }

            if(_timeOnFloor > MaxTimeOnFloor)
            {
                StartCoroutine(Dissolve());
            }
        }

        private IEnumerator ExplodePickable(GameObject explosion)
        {
            enabled = false;
            var images = GetComponentsInChildren<SpriteRenderer>();
            while (true)
            {
                var lastAlpha = 0f;
                var delta = Time.deltaTime * 2;
                foreach (var i in images)
                {
                    i.color = new Color(i.color.r - delta, i.color.g - delta, i.color.b - delta, 1);
                    lastAlpha = i.color.r;
                }
                if (lastAlpha <= 0)
                {
                    break;
                }
                yield return null;
            }
            var ex = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(ex, 0.3f);
        }

        private IEnumerator Dissolve()
        {
            enabled = false;
            var images = GetComponentsInChildren<SpriteRenderer>();
            while (true)
            {
                var lastAlpha = 0f;
                foreach (var i in images)
                {
                    i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - Time.deltaTime * 2);
                    lastAlpha = i.color.a;
                    transform.position += Vector3.up * Time.deltaTime;
                }
                if (lastAlpha <= 0)
                {
                    break;
                }
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
