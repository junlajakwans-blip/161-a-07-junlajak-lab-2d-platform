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


//create HealthBar
    protected virtual void CreateHealthBar() 
    {
        // Wait for HealthBarManager to be ready
        if (HealthBarManager.Instance == null)
        {
            Debug.LogWarning("HealthBarManager not ready yet.");
            return;
        }

        // Avoid duplicate HealthBars
        if (healthBarCanvas != null) return;

        // Create HealthBar
        GameObject hb = HealthBarManager.Instance.GetHealthBar();
        hb.transform.position = transform.position + Vector3.up * 1.2f;

        // Set up FollowHead component
        FollowHead follow = hb.GetComponent<FollowHead>();
        if (follow == null)
            follow = hb.AddComponent<FollowHead>();
        follow.target = transform;
        follow.offset = new Vector3(0, 1.2f, 0);

        // Get references to Canvas and Fill Image
        healthBarCanvas = hb.GetComponent<Canvas>();
        healthBarFillImage = hb.transform.Find("Fill").GetComponent<Image>();

        // Ensure Canvas is in World Space
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

        CreateHealthBar(); // Call CreateHealthBar method
        Debug.Log($"{this.name} initialized with {Health} health.");

    }

#region Damage and Death
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
            if (healthBarCanvas != null) // Return HealthBar to pool
                HealthBarManager.Instance.ReturnHealthBar(healthBarCanvas.gameObject);
            healthBarCanvas = null; // Clear reference

            Destroy(gameObject);
            return true;
        }
        else { return false; }
    }
#endregion

#region Update HealthBar
    protected virtual void UpdateHealthBar() // Update HealthBar method realtime
    {
        if (healthBarFillImage != null)
        {
            float fillAmount = (float)Health / maxHealth;
            healthBarFillImage.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }
#endregion
}
