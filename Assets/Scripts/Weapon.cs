using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class Weapon : MonoBehaviour
{
    public int damage;
    public IShootable Shooter;
    public abstract void Move();
    public abstract void OnHitWith(Character character);

    public void InitWeapon(int newDamage, IShootable newShooter)
    {
        damage = newDamage;
        Shooter = newShooter;
    }

    public int GetShootDirection()
    {
        float value = Shooter.ShootPoint.position.x - Shooter.ShootPoint.parent.position.x; // Calculate direction based on ShootPoint position relative to parent
        if (value > 0)
            return 1; // Right
        else return -1; // Left
        
    }

    public void OnTriggerEnter2D(Collider2D other) // Detect collision with Character
    {
        Character character = other.GetComponent<Character>(); // Check if the collided object has a Character component
        if (character != null)
        {
            OnHitWith(character); // Call OnHitWith when colliding with a Character
            Destroy(this.gameObject, 5f); // Destroy the weapon after hitting a character (after 5 seconds)
            //Destroy(this.gameObject); // Immediate destroy
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
