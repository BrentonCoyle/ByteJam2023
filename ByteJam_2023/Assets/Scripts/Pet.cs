using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [SerializeField] private int age;
    [SerializeField] private int hunger;
    [SerializeField] private string pName; // p = Pet
    [SerializeField] private bool isSick = false;
    [SerializeField] private SpriteRenderer sr;
}
