using UnityEngine;

public class PlayerMovement : PlayerBehaviour {

    public float Speed;
    private Rigidbody2D rb;
    public Vector2 Velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Velocity = new Vector2();
    }

    void FixedUpdate () {
        var horizontal = PlayerInput.GetAxis("Horizontal", Player) * Speed;
        var vertical = PlayerInput.GetAxis("Vertical", Player) * Speed;
        Velocity = new Vector2(horizontal, vertical);
        rb.velocity = Velocity;
    }
}
