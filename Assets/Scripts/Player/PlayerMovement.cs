using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerMovement : MonoBehaviour {

        public float Speed;
        private Rigidbody2D _rb;
        public Vector2 Velocity;
        public AudioClip Bump;
        private PlayerBehaviour _player;
        private bool _isStopped;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            Velocity = new Vector2();
            _player = GetComponent<PlayerBehaviour>();
        }

        public void Stop()
        {
            _isStopped = true;
            _rb.velocity = Vector2.zero;
        }

        private void FixedUpdate () {
            if (_isStopped) return;

            var horizontal = PlayerInput.GetAxis("Horizontal", _player.Player) * Speed;
            var vertical = PlayerInput.GetAxis("Vertical", _player.Player) * Speed;
            Velocity = new Vector2(horizontal, vertical);
            _rb.velocity = Velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(Bump, FindObjectOfType<AudioSource>().transform.position);
            }
        }
    }
}
