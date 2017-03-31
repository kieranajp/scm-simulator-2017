using UnityEngine;

public class PlayerMovement : PlayerBehaviour {

    public float Speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        var horizontal = PlayerInput.GetAxis("Horizontal", Player) * Speed;
        var vertical = PlayerInput.GetAxis("Vertical", Player) * Speed;
        rb.velocity = new Vector3(horizontal, vertical, 0);
    }
}
