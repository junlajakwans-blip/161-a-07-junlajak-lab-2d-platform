using NUnit.Framework;
using UnityEngine;

public class Player : Character, IShootable
{
    [field: SerializeField] public GameObject Bullet { get; set; }
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
        //Debug.Log("Fixed Update " + Time.fixedDeltaTime);
        WaitTime += Time.fixedDeltaTime; // = 0 + 0.02 + 0.02 ... 
    }

    public void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && WaitTime >= ReloadTime)
        {
            var bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
            Banana banana = bullet.GetComponent<Banana>();
            if (banana != null)
               banana.InitWeapon(20, this); //set banana damage to 20
            WaitTime = 0.0f; //reset wait time
        }
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
            OnHitWith(enemy);
            Debug.Log($"{this.name} Hit with {enemy.name}!");
        }
    }   
    
}
