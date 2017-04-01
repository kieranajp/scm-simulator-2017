using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float Speed;
    private Rigidbody2D rb;
    public Vector2 Velocity;
    public AudioClip bump;
    public AudioClip footstepSound;
    private AudioSource source;
    private PlayerBehaviour player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Velocity = new Vector2();
        source = GetComponent<AudioSource>();
        player = GetComponent<PlayerBehaviour>();
    }

    void FixedUpdate () {
        var horizontal = PlayerInput.GetAxis("Horizontal", player.Player) * Speed;
        var vertical = PlayerInput.GetAxis("Vertical", player.Player) * Speed;
        Velocity = new Vector2(horizontal, vertical);
        rb.velocity = Velocity;

        if (Velocity.magnitude < 0.1f) {
            source.volume = 0;
        } else {
            source.volume = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(bump, FindObjectOfType<AudioSource>().transform.position);
    }
}
