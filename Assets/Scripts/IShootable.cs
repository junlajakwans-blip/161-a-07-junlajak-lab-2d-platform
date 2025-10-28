using UnityEngine;

public interface IShootable
{
    public GameObject bullet { get; set; }
    public Transform ShootPoint { get; set; }
    public float ReloadTime { get; set; }
    public float WaitTime { get; set; }
    public void Shoot();
}
