using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    private static MainCanvas canvas = null;

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = this;
        }
        else
        {
            if (canvas != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
