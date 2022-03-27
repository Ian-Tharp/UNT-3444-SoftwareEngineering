using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverButtons : MonoBehaviour
{
    [SerializeField]
    GameObject goBut;

    public void GameOver()
    {
        goBut.SetActive(true);

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("FirstMap");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
