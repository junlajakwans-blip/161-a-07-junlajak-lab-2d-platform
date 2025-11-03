using UnityEngine;
using System.Collections.Generic;

//nanage HealthBar
public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager Instance;
    [SerializeField] private GameObject healthBarPrefab;
    private readonly Queue<GameObject> pool = new Queue<GameObject>();

//called when the script instance is being loaded
    void Awake()
    {
        Instance = this;
    }

//get HealthBar 
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

//check out HealthBar and deactivate it
    public void ReturnHealthBar(GameObject hb)
    {
        hb.SetActive(false);
        pool.Enqueue(hb);
    }
}
