using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [SerializeField] private int age = 0;
    [SerializeField] private int hunger = 0;
    [SerializeField] private string pName; // p = Pet
    [SerializeField] private bool isSick = false;
    [SerializeField] private Sprite sprite;
}
