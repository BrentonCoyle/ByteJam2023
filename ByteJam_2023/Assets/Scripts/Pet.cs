using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    [SerializeField] private int age;
    [SerializeField] private string pName; // p = Pet
    public Text text;

    private int hunger = 100;
    private int health = 100;
    private int CostOfFood = 10;
    private int CostOfMedical = 10;
    private int Money = 100;


    [SerializeField] private SpriteRenderer sr;

    private int HungerTime = 100;

    public int Increase = 5;
    public int Decrease = 5;
    public Image HealthBarGreen;
    public Image FoodBarGreen;



    
    private void Update()
    {

        // if the hunger timer is lower than 0 decrease the food amount.
        if(HungerTime > 0)
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
