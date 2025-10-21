using UnityEngine;

public abstract class Enemy : Character
{
    //auto-property
    public int DamageHit { get; protected set; }
    //abstract method for enemy
    public abstract void Behavior();

}
