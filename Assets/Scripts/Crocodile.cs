using UnityEngine;

public class Crocodile : Enemy
{
    // Crocodile-specific implementation
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float atkRange = 1.0f;
    public Player player;

    void Start()
    {
        base.Initialize(30); // Initialize with 30 health
        DamageHit = 30;
        atkRange = 6.0f;
        player = GameObject.FindFirstObjectByType<Player>();
    }

    private void FixedUpdate()
    {
        Behavior();
    }

    public override void Behavior()
    {
        Vector2 distance = transform.position - player.transform.position;

        if (distance.magnitude <= atkRange)
        {
            // Attack
            Debug.Log($"{player.name} is in {this.name}'s attack range! Attacking!");
            Shoot();
        }

    }

    public void Shoot()
    {
        Debug.Log($"{this.name} shoots a rock at {player.name}!");
    }
}
