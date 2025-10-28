using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    //attributes
    private int maxHealth;

    [Header("Health Bar")]
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Canvas healthBarCanvas;
    private FollowHead followScript; // Reference to FollowHead script

    private int health;
    public int Health 
    { 
    get => health;
    set
        {
            health = Mathf.Max(0, value);
            UpdateHealthBar(); 
        }
    }
    protected Animator anim;
    protected Rigidbody2D rb;
    

    protected virtual void CreateHealthBar()
    {
        // 🔹รอจนกว่า Manager จะพร้อม
        if (HealthBarManager.Instance == null)
        {
            Debug.LogWarning("HealthBarManager not ready yet.");
            return;
        }

        // 🔹กัน spawn ซ้ำ
        if (healthBarCanvas != null) return;

        // 🔹สร้าง HealthBar
        GameObject hb = HealthBarManager.Instance.GetHealthBar();
        hb.transform.position = transform.position + Vector3.up * 1.2f;

        // 🔹ตั้ง follow target
        FollowHead follow = hb.GetComponent<FollowHead>();
        if (follow == null)
            follow = hb.AddComponent<FollowHead>();
        follow.target = transform;
        follow.offset = new Vector3(0, 1.2f, 0);

        // 🔹อ้างอิง UI ภายใน
        healthBarCanvas = hb.GetComponent<Canvas>();
        healthBarFillImage = hb.transform.Find("Fill").GetComponent<Image>();

        // 🔹บังคับให้ Canvas เป็น World Space
        if (healthBarCanvas.renderMode != RenderMode.WorldSpace)
            healthBarCanvas.renderMode = RenderMode.WorldSpace;

        UpdateHealthBar();
    }

    //initialize character
    public void Initialize(int startHealth)
    {
        // Set starting health
        maxHealth = startHealth;
        Health = startHealth;

        // Get components
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        CreateHealthBar();
        Debug.Log($"{this.name} initialized with {Health} health.");

    }

    //Behavior
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{this.name} took {damage} damage, remaining health: {Health}");
        IsDead();
    }

    public bool IsDead()
    {
        if (Health <= 0)
        {
            if (healthBarCanvas != null)
                HealthBarManager.Instance.ReturnHealthBar(healthBarCanvas.gameObject);
                healthBarCanvas = null;

            Destroy(gameObject);
            return true;
        }
        else { return false; }
    }
    
    protected virtual void UpdateHealthBar()
    {
        if (healthBarFillImage != null)
        {
            float fillAmount = (float)Health / maxHealth;
            healthBarFillImage.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }
}
