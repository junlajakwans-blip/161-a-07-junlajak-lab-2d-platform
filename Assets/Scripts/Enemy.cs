using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //auto-property
    public int DamageHit { get; set; }
    //abstract method for enemy
    public abstract void Behavior();

    private int health;
    public int Health 
    { 
    get => health; 
    set => health = (value < 0) ? 0 : value; 
    }
    protected Animator anim;
    protected Rigidbody2D rb;
    //initialize character
    public void Initialize(int startHealth)
    {
        // Set starting health
        Health = startHealth;
        Debug.Log($"{this.name} initialized with {Health} health.");

        // Get components
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
}
