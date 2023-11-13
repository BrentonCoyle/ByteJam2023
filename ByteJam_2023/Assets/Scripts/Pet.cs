using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    [SerializeField] private string pName; // p = Pet   

    [SerializeField] private float maxVelocity;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private float hatchTime = 0;
    [SerializeField] private float hungerTime = 100;
    [SerializeField] private float sickTime = 100;
    [SerializeField] private float hunger = 100;
    [SerializeField] private float health = 100;

    [SerializeField] private int age = 0;
    [SerializeField] private int costOfFood = 10;
    [SerializeField] private int costOfMedical = 10;
    [SerializeField] private int increase = 5;
    [SerializeField] private int decrease = 5;

    private bool isHatched = false;
    private bool isSick = false;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Text text;
    [SerializeField] private Image HealthBarGreen;
    [SerializeField] private Image FoodBarGreen;

    [SerializeField] private Sprite[] petSprites;


    private void Awake()
    {
        int randPetSpriteIndex = Random.Range(0, petSprites.Length);
        StartCoroutine(Hatch(randPetSpriteIndex));
    }

    private void Update()
    {
        // if the hunger timer is lower than 0 decrease the food amount.
        if (isHatched)
        {
            if (hunger > 0) { DecreaseFoodStat(); }
            else { DecreaseHealthStat(); }

            if (!isSick)
            {
                if (sickTime > 0) { sickTime -= (100 * Time.deltaTime); }
                else { TrySick(); }
            }
            else { DecreaseHealthStat(); }
        }      
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var angle = minAngle + Random.Range(50, 100) * (maxAngle - minAngle);
        var x = Mathf.Cos(angle);
        var y = Mathf.Sin(angle);
        rb.AddForce(new Vector2(x, y) * 25);
    }

    // Increase the food amount/ bar IF you have the money
    public void IncreaseFoodStat()
    {
        int playerMoney = PlayerManager.GetMoney();
        if (playerMoney >= costOfFood)
        {
            hunger += increase;
            FoodBarGreen.fillAmount = hunger / 100f;
            hunger = Mathf.Clamp(hunger, 0, 100);
            PlayerManager.ChangeMoney(playerMoney - costOfFood);
        }
    }

    public void UseMedicine()
    {
        if (PlayerManager.GetMoney() >= costOfMedical && isSick)
        {
            sr.color = new Color(255, 255, 255, 255);
            isSick = false;
        }
    }

    private IEnumerator Hatch(int indexSelection)
    {
        yield return new WaitForSeconds(hatchTime);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        SetSprite(indexSelection);
        isHatched = true;
    }

    private void SetSprite(int indexSelection)
    {
        if (indexSelection < petSprites.Length && indexSelection >= 0) { sr.sprite = petSprites[indexSelection]; }
        else { return; }  
    }

    // decrease the food amount and update the bar
    private void DecreaseFoodStat()
    {
        if (hunger > 0)
        {
            hunger -= decrease * Time.deltaTime;
            FoodBarGreen.fillAmount = hunger / 100f;
        }
        else { return; }    
    }

    // Same thing as food but for health.
    private void DecreaseHealthStat()
    {
        if (health > 0)
        {
            health -= decrease * Time.deltaTime;
            HealthBarGreen.fillAmount = health / 100f;
        }
        else { Die(); }
    }

    private void IncreaseHealthStat()
    {
        int playerMoney = PlayerManager.GetMoney();
        if (playerMoney >= costOfFood)
        {
            health += increase;
            HealthBarGreen.fillAmount = health / 100f;
            health = Mathf.Clamp(health, 0, 100);
            PlayerManager.ChangeMoney(playerMoney - costOfFood);
        }
        
    }

    private void TrySick()
    {
        int randNum = Random.Range(0, 10);
        if (randNum == 0)
        {
            sr.color = Color.green;
            isSick = true;
        }
        else { sickTime = 100; }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
