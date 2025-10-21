using NUnit.Framework;
using UnityEngine;

public class Player : Character
{
    // Player-specific implementation
    void Start()
    {
        base.Initialize(100); //set Player's Health to 100
    }
    
    public void OnHitWith(Enemy enemy)
    {
        TakeDamage(enemy.DamageHit);
        IsDead();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log($"{this.name} Hit with {enemy.name}!");
            OnHitWith(enemy);
        }
    }   
    
}
