using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerController : MonoBehaviour
{
    private static ManagerController mmController = null;

    private void Awake()
    {
        if (mmController == null)
        {
            mmController = this;
        }
        else
        {
            if (mmController != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
        InitManagers();
    }

    private void InitManagers()
    {
        PlayerManager.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PetManager.pet = GameObject.FindGameObjectWithTag("Pet").GetComponent<Pet>();
        CanvasManager.canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MainCanvas>();
    }
}
