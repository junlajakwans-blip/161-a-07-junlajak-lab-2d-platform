using UnityEngine;

public class Banana : Weapon
{
    [SerializeField] private float speed;
    public override void Move()
    {
        // Implement banana-specific movement logic
        float newX = transform.position.x + speed * Time.fixedDeltaTime; // Move banana horizontally based on speed
        float newY = transform.position.y; // Keep the same vertical position
        Vector2 newPosition = new Vector2(newX, newY);
        transform.position = newPosition;
    }

    public override void OnHitWith(Character character)
    {
        // Implement banana-specific hit logic
        if (character is Enemy)
            character.TakeDamage(this.damage);
    }

    void Start()
    {
        speed = 4.0f * GetShootDirection(); // Set speed based on shoot direction
        damage = 30; // Default damage
    }

    private void FixedUpdate()
    {
        Move(); // Call Move method every fixed frame
    }
}
