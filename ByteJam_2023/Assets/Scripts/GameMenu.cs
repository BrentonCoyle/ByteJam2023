using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        CanvasManager.canvas.GetComponent<Canvas>().enabled = false;
        PetManager.pet.GetComponent<SpriteRenderer>().enabled = false;
        PetManager.pet.GetComponent<Pet>().enabled = false;
        SceneManager.LoadScene("TicTacToe");
    }
}
