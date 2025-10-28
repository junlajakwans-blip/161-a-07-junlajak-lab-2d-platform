using UnityEngine;

public class Crocodile : Enemy , IShootable
{
    // Crocodile-specific implementation
    [SerializeField] private float atkRange;
    public Player player; //static reference to Player instance

    //auto-property
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }
    public float ReloadTime { get; set; }
    public float WaitTime { get; set; }

    private bool isPlayerInRange = false; //Check if Player is in attack range

    void Start()
    {
        base.Initialize(50); //set Enemy's Health to 50
        DamageHit = 30; //set Enemy's DamageHit to 30
        atkRange = 6.0f; //set Enemy's attack range to 6.0f
        player = GameObject.FindFirstObjectByType<Player>(); //set static Player reference

        ReloadTime = 2.0f;
        WaitTime = 5.0f;

    }

    private void FixedUpdate()
    {
        if (player == null) return; 
        WaitTime += Time.fixedDeltaTime; // = 0 + 0.02 + 0.02 ... 
        Behavior(); //call Behavior method
    }
    
    public override void Behavior()
    {
        if (player == null)
            return;
        
        //find distance between Croccodile and Player
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // ✅ Player เข้ามาในระยะครั้งแรก
        if (!isPlayerInRange && distance <= atkRange)
        {
            isPlayerInRange = true;
            Debug.Log($"{player.name} just ENTERED {this.name}'s attack range!");
        }

        // ✅ Player ออกนอกระยะ
        if (isPlayerInRange && distance > atkRange)
        {
            isPlayerInRange = false;
            Debug.Log($"{player.name} left {this.name}'s attack range!");
        }

        // ✅ อยู่ในระยะ ยิงเฉพาะเมื่อครบเวลา reload
        if (isPlayerInRange && WaitTime >= ReloadTime)
        {
            Shoot();
            WaitTime = 0.0f;
        }
    }

    public void Shoot()
    {
        if (WaitTime >= ReloadTime)
        {
            anim.SetTrigger("Shoot");
            var bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
            Rock rock = bullet.GetComponent<Rock>();
            rock.InitWeapon(30, this); // ให้ Rock รู้ว่าใครเป็น Shooter
            WaitTime = 0.0f;
        }
    }
}
