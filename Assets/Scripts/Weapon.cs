using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage;
    public abstract void Move();
    public abstract void OnHitWith(Character character);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
