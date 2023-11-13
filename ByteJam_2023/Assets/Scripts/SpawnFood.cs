using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{

    [SerializeField] public GameObject Food;
    // Start is called before the first frame update
    public void Spawnfood()
    {
        Instantiate(Food, transform.position, Quaternion.identity);
    }



}
