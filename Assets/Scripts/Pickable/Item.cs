using System.Collections;
using UnityEngine;

namespace Pickable
{
    public class Item : Pickable
    {
        public IEnumerator StartPulsating()
        {
            while (!IsPickedUp)
            {
                var flash = Mathf.PingPong(Time.time * 10, 1);
                SpriteRenderer.color = new Color(1, flash, flash);
                yield return null;
            }
        }
    }
}