using UnityEditor.Callbacks;
using UnityEngine;

public class Ant : Enemy
{
    [SerializeField] private Vector2 velocity;
    public Transform[] movePoints;

    void Start()
    {
        base.Initialize(15); // Initialize with 15 health
        DamageHit = 10;
        velocity = new Vector2(-1.0f, 0.0f); // Default velocity

    }

    public override void Behavior()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        if (velocity.x < 0 && transform.position.x <= movePoints[0].position.x)
        {
            Flip();
        }
        else if (velocity.x > 0 && transform.position.x >= movePoints[1].position.x)
        {
            Flip();
        }
    }

    public void Flip()
    {
        velocity.x *= -1;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        Behavior();
    }

}

