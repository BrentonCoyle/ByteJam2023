using System.Collections;
using TMPro;
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
    [SerializeField] private float age = 0;

    [SerializeField] private int costOfFood = 10;
    [SerializeField] private int costOfMedical = 10;
    [SerializeField] private int increase = 20;
    [SerializeField] private int decrease = 3;

    private bool isHatched = false;
    private bool isSick = false;
    private bool isJuvinile = false;
    private bool isAdult = false;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Text text;
    [SerializeField] private Image HealthBarGreen;
    [SerializeField] private Image FoodBarGreen;

    [SerializeField] private Sprite[] petSprites;
    [SerializeField] private Sprite[] batStageSprites;
    [SerializeField] private Sprite[] catStageSprites;

    [SerializeField] private TMP_Text MoneyText;
    [SerializeField] private TMP_Text FoodCostText;
    [SerializeField] private TMP_Text MediceneCostText;

    [SerializeField] private GameObject FoodSpawner;
    [SerializeField] private GameObject MediceneSpawner;

    [SerializeField] private GameObject Food;
    [SerializeField] private GameObject Pill;


    private void Awake()
    {
        int randPetSpriteIndex = Random.Range(0, petSprites.Length);
        StartCoroutine(Hatch(randPetSpriteIndex));
    }

    private void Update()
    {


        /// Text for UI
        MoneyText.SetText("Current Money: $" + PlayerManager.GetMoney());
      FoodCostText.SetText("Cost: $" + costOfFood);
      MediceneCostText.SetText("Cost: $" + costOfMedical);

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

            age += 0.1f * Time.deltaTime;
            if (age >= 3 && !isJuvinile && !isAdult)
            {
                // Juvenile
                if (sr.sprite == petSprites[0])
                {
                    sr.sprite = batStageSprites[0];
                }
                
                else if (sr.sprite == petSprites[2])
                {
                    sr.sprite = catStageSprites[0];
                }
                isJuvinile = true;
            }
            else if (age >= 6 && !isAdult)
            {
                // Adult
                if (sr.sprite == batStageSprites[0])
                {
                    sr.sprite = batStageSprites[1];
                }
                else if (sr.sprite == catStageSprites[0])
                {
                    sr.sprite = catStageSprites[1];
                }
                isAdult = true;
                isJuvinile = false;
            }
        }
    }


    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Medicene(Clone)")
        {
            IncreaseHealthStat();
            UseMedicine();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Food(Clone)")
        {
            IncreaseFoodStat();
            Destroy(collision.gameObject);
        }


        if (GameObject.Find("Medicene(Clone)"))
        {
            rb.AddForce(new Vector2(15, 5) * 10);

        }
        else
        {
            if (GameObject.Find("Food(Clone)"))
            {
                rb.AddForce(new Vector2(-15, 5) * 10);
            }
        }
        var angle = minAngle + Random.Range(50, 100) * (maxAngle - minAngle);
        var x = Mathf.Cos(angle);
        var y = Mathf.Sin(angle);
        rb.AddForce(new Vector2(x, y) * 25);
    }

    // Increase the food amount/ bar IF you have the money
    public void IncreaseFoodStat()
    {      
        hunger += increase;
        FoodBarGreen.fillAmount = hunger / 100f;
        hunger = Mathf.Clamp(hunger, 0, 100);       
    }

    public void UseMedicine()
    {       
        sr.color = new Color(255, 255, 255, 255);
        isSick = false;
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
        health += increase;
        HealthBarGreen.fillAmount = health / 100f;
        health = Mathf.Clamp(health, 0, 100);            
    }

    public void DecreaseFoodMoney()
    {
        int money = PlayerManager.GetMoney();
        if (money >= costOfFood)
        {
            PlayerManager.ChangeMoney(money - costOfFood);
            FoodSpawner.GetComponent<SpawnFood>().Spawnfood();
        }
        
    }

    private void TrySick()
    {
        int randNum = Random.Range(0, 20);
        if (randNum == 0)
        {
            sr.color = Color.green;
            isSick = true;
        }
        else { sickTime = 100; }
    }

    public void DecreaseMedMoney()
    {
        int money = PlayerManager.GetMoney();
        if (money >= costOfMedical)
        {
            PlayerManager.ChangeMoney(money - costOfMedical);
            MediceneSpawner.GetComponent<SpawnMedicene>().Spawnmedicene();
        }
    }      

    private void Die()
    {
        Destroy(gameObject);
    }
}
