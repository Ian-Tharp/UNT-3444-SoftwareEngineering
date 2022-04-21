using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverButtons : MonoBehaviour
{
    [SerializeField]
    GameObject goBut;
    [SerializeField] WaveManager wm;
    [SerializeField] PlayerStats ps;
    [SerializeField] CardMenu cm;
    
    public Text wvTxt;
    public Text scTxt;
    public Text tmTxt;
    public Text upTxt;
    public Text gmTxt;

    public void GameOver()
    {
        wvTxt.text = "Waves completed: " + wm.WaveNumber;
        scTxt.text = "Score: " + ps.Score;
        tmTxt.text = "Time: " + Time.fixedTime + " sec";

        upTxt.text = cm.upgradeStats[0].ToString() + "\n" + cm.upgradeStats[1].ToString() + "\n";
        upTxt.text += cm.upgradeStats[2].ToString() + "\n" + cm.upgradeStats[3].ToString() + "\n";
        upTxt.text += cm.upgradeStats[4].ToString() + "\n" + cm.upgradeStats[5].ToString() ;
        
        gmTxt.text = ps.killed + "\n" + ps.eliteKilled + "\n";
        gmTxt.text += ps.explosions + "\n" + ps.shots + "\n";
        gmTxt.text += ps.bloodSpilled.ToString("F1") + " GAL\n0";
        
        
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
