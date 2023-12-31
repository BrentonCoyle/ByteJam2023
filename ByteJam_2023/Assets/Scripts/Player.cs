using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int money = 100;
    private static Player player = null;
    [SerializeField] private List<Pet> pets;

    private void Awake()
    {
        if (player == null)
        {
            player = this;
        }
        else
        {
            if (player != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
