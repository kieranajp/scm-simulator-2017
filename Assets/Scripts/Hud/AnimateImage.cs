using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(SpriteRenderer))]
    [ExecuteInEditMode]
    public class AnimateImage : MonoBehaviour
    {
        private Image _image;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _image = GetComponent<Image>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Update ()
        {
            _image.sprite = _spriteRenderer.sprite;
        }
    }
}
