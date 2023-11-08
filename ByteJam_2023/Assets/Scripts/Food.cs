using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] int hungerPoints;
    [SerializeField] int price;
    [SerializeField] string fName; // f = food
    [SerializeField] private SpriteRenderer sr;
}
