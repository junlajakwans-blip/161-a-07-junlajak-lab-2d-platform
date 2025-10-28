using NUnit.Framework;
using UnityEngine;

public class Player : Character, IShootable
{
    //implement IShootable interface   
    [field: SerializeField] public GameObject bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }
    public float ReloadTime { get; set; }
    public float WaitTime { get; set; }



    // Player-specific implementation
    void Start()
    {
        base.Initialize(100); //set Player's Health to 100
        ReloadTime = 1.0f;
        WaitTime = 0.0f;
    }

    public void FixedUpdate() //loop  every 0.02 seconds
    {
        WaitTime += Time.fixedDeltaTime; // = 0 +0 .02 + 0.02 ...
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
