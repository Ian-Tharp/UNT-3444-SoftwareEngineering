using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int Currency = 0;
    public int TotalHealth = 100;
    public int CurrentHealth = 0;
    public int CurrentShields = 0;
    public int TotalShields = 0;

    public bool hasShield = false;
    bool isDead = false;

    public WaveManager waveManager;

    //--------------------------------------------------------------------------------
    //Additions
    public void AddCurrency(int Amount) {
        Currency += Amount;
        //Max Currency Check
        if (Currency > 999999) {
            Currency = 999999;
        }
    }

    public void AddCurrentHealth(int Amount) {
        CurrentHealth += Amount;
        //Addition overflow check
        if (CurrentHealth > TotalHealth) {
            CurrentHealth = TotalHealth;
        }
    }

    public void AddTotalHealth(int Amount) {
        TotalHealth += Amount;
        //Total Health overflow check
        if (TotalHealth > 200) {
            TotalHealth = 200;
        }
        AddCurrentHealth(Amount);
    }

    //--------------------------------------------------------------------------------
    //Subtractions
    public void SubtractCurrentHealth(int Amount) {
        CurrentHealth -= Amount;
        if (CurrentHealth <= 0) {
            isDead = true;
        }
    }

    public void SubtractTotalHealth(int Amount) {
        TotalHealth -= Amount;
        //Cannot reduce total health below 10
        if (TotalHealth < 10) {
            TotalHealth = 10;
        }
    }

    public bool TryToSubtractCurrency(int Amount) {
        if (Currency - Amount >= 0) {
            return true;
        }
        else {
            return false;
        }
    }

    public void ResetHealth() { CurrentHealth = TotalHealth; }
    public void ResetCurrency() { Currency = 0; }
    public void ResetShields() { CurrentShields = TotalShields; }

    //Market screen callable function for when player purchases shields
    public void EnableMaxShields() {
        hasShield = true;
        CurrentShields = 100;
        TotalShields = 100;
    }
    
    public void EnableMediumShields() { 
        hasShield = true;
        CurrentShields = 50;
        TotalShields = 50;
    }

    public void EnableSmallShields() {
        hasShield = true;
        CurrentShields = 25;
        TotalShields = 25;
    }

    //Constructor
    void Start() {
        CurrentHealth = TotalHealth;
    }

    //Called once every frame
    void Update() {
        //Checks if player isDead bool is true
        //If so, quit the game for now
        //We will implement death screen functionality later
        if (isDead) {
            Application.Quit();
        }
    }
}
