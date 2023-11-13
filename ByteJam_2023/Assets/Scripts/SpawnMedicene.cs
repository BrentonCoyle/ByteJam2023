using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnMedicene : MonoBehaviour
{

    [SerializeField] public GameObject Medicene;
    // Start is called before the first frame update
    

    public void Spawnmedicene()
    {
        Instantiate(Medicene, transform.position, Quaternion.identity);
    }


}
