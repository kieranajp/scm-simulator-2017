using UnityEngine;

public class PlayerMovement : PlayerBehaviour {

    public float Speed;
    private Rigidbody2D rb;
    public Vector2 Velocity;
    public AudioClip bump;
    public AudioClip footstepSound;
    private AudioSource source;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Velocity = new Vector2();
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate () {
        var horizontal = PlayerInput.GetAxis("Horizontal", Player) * Speed;
        var vertical = PlayerInput.GetAxis("Vertical", Player) * Speed;
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
