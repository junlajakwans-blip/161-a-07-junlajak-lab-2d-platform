using UnityEngine;
using System.Collections.Generic;

public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager Instance;
    [SerializeField] private GameObject healthBarPrefab;
    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetHealthBar()
    {
        if (pool.Count > 0)
        {
            var hb = pool.Dequeue();
            hb.SetActive(true);
            return hb;
        }
        else
        {
            return Instantiate(healthBarPrefab);
        }
    }

    public void ReturnHealthBar(GameObject hb)
    {
        hb.SetActive(false);
        pool.Enqueue(hb);
    }
}
