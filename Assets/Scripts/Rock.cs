using UnityEngine;

public class Rock : Weapon
{
    public Rigidbody2D rb;
    public Vector2 force;
    public override void Move()
    {
        // Implement rock-specific movement logic
        rb.AddForce(force);
    }

    public override void OnHitWith(Character obj)
    {
        // Implement rock-specific hit logic
        if (obj is Player)
            obj.TakeDamage(this.damage);
    }

    void Start()
    {
        damage = 40; // Default damage
        force = new Vector2(5.0f * GetShootDirection() * 40, 300); // Set force based on shoot direction
        Move(); // Apply force to the rock
    }
}
