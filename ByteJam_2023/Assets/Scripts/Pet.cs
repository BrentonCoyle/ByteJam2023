using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    [SerializeField] private Sprite[] petSprites;

    [SerializeField] private int age;
    [SerializeField] private string pName; // p = Pet
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    public Text text;

    private int hunger = 100;
    private int health = 100;
    private int CostOfFood = 10;
    private int CostOfMedical = 10;
    private int Money = 100;
    private int HungerTime = 100;

    public int Increase = 5;
    public int Decrease = 5;
    public Image HealthBarGreen;
    public Image FoodBarGreen;


    private void Awake()
    {
        SetRandomSprite();
    }

    private void Update()
    {
        // if the hunger timer is lower than 0 decrease the food amount.
        if (HungerTime > 0)
        {
            HungerTime -= 1;
        }
        else
        {
            DecreaseFoodStat();

            if (hunger <= 75)
            {
                DecreaseHealthStat();
            }
            HungerTime = 100;
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

    private void SetRandomSprite()
    {
        int randIndex = Random.Range(0, 3);
        sr.sprite = petSprites[randIndex];
    }

    // decrease the food amount and update the bar
    public void DecreaseFoodStat()
    {
        hunger -= Decrease;
        FoodBarGreen.fillAmount = hunger / 100f;
    }

    // Increase the food amount/ bar IF you have the money
    public void IncreaseFoodStat()
    {
        if(Money >= CostOfFood)
        {
            hunger += Increase;
            FoodBarGreen.fillAmount = hunger / 100f;
            hunger = Mathf.Clamp(hunger, 0, 100);
            Money -= CostOfFood;
        }
        
    }

    // Same thing as food but for health.
    public void DecreaseHealthStat()
    {
        health -= Decrease;
        HealthBarGreen.fillAmount = health / 100f;
    }

    public void IncreaseHealthStat()
    {
        if(Money >= CostOfMedical)
        {
            health += Increase;
            HealthBarGreen.fillAmount = health / 100f;
            health = Mathf.Clamp(health, 0, 100);
            Money -= CostOfMedical;
        }
        
    }
}
