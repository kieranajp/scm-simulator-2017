using System.Collections;
using UnityEngine;

namespace Box
{
    public class BoxSpawner : MonoBehaviour
    {
        public Transform BoxesInCorner;
        public GameObject Box;

        public float SpawnDelay = 3f;
        public float Delay;

        private Box _currentBox;

        private void Start()
        {
            StartCoroutine(CreateBox());
        }

        private void Update()
        {
            if (_currentBox == null || _currentBox.HasBeenPicked)
            {
                Delay += Time.deltaTime;
            }

            if (Delay > SpawnDelay)
            {
                StartCoroutine(CreateBox());
                Delay = 0f;
            }
        }

        private IEnumerator CreateBox()
        {
            var currentYOffser = 0f;
            var yOffset = 3f;
            var box = Instantiate(Box, transform.position - new Vector3(0, yOffset, 0), Quaternion.identity);
            _currentBox = box.GetComponent<Box>();
            _currentBox.enabled = false;
            _currentBox.GetComponent<Collider2D>().enabled = false;

            var delta = 3 * Time.deltaTime;
            while (currentYOffser < yOffset)
            {
                for (var i = 0; i < BoxesInCorner.childCount; i++)
                {
                    var c = BoxesInCorner.GetChild(i);
                    if (c.GetComponent<SpriteRenderer>() != null)
                    {
                        var sign = 1;
                        if (i % 2 == 0)
                        {
                            sign = -1;
                        }
                        var sin = sign * Mathf.Sin(Time.unscaledDeltaTime * 7);
                        c.Rotate(Vector3.forward, sin);
                    }
                }

                box.transform.position += Vector3.up * delta;
                currentYOffser += delta;
                yield return null;
            }
            _currentBox.enabled = true;
            _currentBox.GetComponent<Collider2D>().enabled = true;
        }
    }
}