using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public class NotificationManager : MonoBehaviour {

        public Image Avatar;
        public Text Title;

        public RandomEvent[] Events;
        private bool _wasFired;
        private const float Duration = 4f;
        private RectTransform _self;

        public void OnEventFired(RandomEvent e) {
            _self = GetComponent<RectTransform>();
            Title.text = e.Message;
            Avatar.sprite = e.Avatar;
            StopAllCoroutines();
            StartCoroutine(Show());
        }

        private IEnumerator Show()
        {
            while (_self.localScale.y < 1)
            {
                _self.localScale = new Vector3(1, Mathf.Lerp(_self.localScale.y, 1.1f, Time.deltaTime * 3), 1);
                yield return null;
            }

            _self.localScale = new Vector3(1, 1, 1);
            yield return new WaitForSeconds(Duration);
            yield return Hide();
        }

        private IEnumerator Hide()
        {

            while (_self.localScale.y > 0)
            {
                yield return null;
                _self.localScale = new Vector3(1, Mathf.Lerp(_self.localScale.y, -0.1f, Time.deltaTime * 3), 1);
            }

            _self.localScale = new Vector3(1, 0, 1);
        }

    }
}