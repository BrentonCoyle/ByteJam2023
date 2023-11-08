using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public int Health = 100;
    public int Food = 100;
    public int Increase = 5;
    public int Decrease = 5;


    public Image HealthBarGreen;
    public Image FoodBarGreen;



    // Update is called once per frame
    void Update()
    {
        // Uses keys W to increase the green on the Health Bar and the S to lower
        if (Input.GetKeyDown(KeyCode.W))
        {
            IncreaseHealthStat();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            DecreaseHealthStat();
        }

        // Uses keys E to increase the green on the Health Bar and the D to lower
        if (Input.GetKeyDown(KeyCode.E))
        {
            IncreaseFoodStat();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DecreaseFoodStat();
        }
    }

    public void DecreaseFoodStat()
    {
        Food -= Decrease;
        FoodBarGreen.fillAmount = Food / 100f;
    }

    public void IncreaseFoodStat()
    {
        Food +=  Increase;
        FoodBarGreen.fillAmount = Food / 100f;
        Food = Mathf.Clamp(Food, 0, 100);
    }

    public void DecreaseHealthStat()
    {
        Health -= Decrease;
        HealthBarGreen.fillAmount = Health / 100f;
    }

    public void IncreaseHealthStat()
    {
        Health += Increase;
        HealthBarGreen.fillAmount = Health / 100f;
        Health = Mathf.Clamp(Health, 0, 100);
    }

}
