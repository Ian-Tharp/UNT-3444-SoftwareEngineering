using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int Currency = 0;
    public int TotalHealth = 100;
    public int CurrentHealth = 0;
    public int CurrentShields = 0;
    public int TotalShields = 0;

    bool hasShield = false;
    bool isDead = false;


    public void SubtractHealth(int Amount) {
        CurrentHealth -= Amount;
        if (CurrentHealth <= 0) {
            isDead = true;
        }
    }
    
    public void AddCurrency(int Amount) {
        Currency += Amount;
        //Max Currency Check
        if (Currency > 999999) {
            Currency = 999999;
        }
    }

    public bool SubtractCurrency(int Amount) {
        if (Currency - Amount >= 0) {
            return true;
        }
        else {
            return false;
        }
    }

    void ResetHealth() { CurrentHealth = TotalHealth; }
    void ResetCurrency() { Currency = 0; }
    void ResetShield() { CurrentShields = TotalShields; }
    

    void EnableShields() { 
        hasShield = true;
        CurrentShields = 50;
        TotalShields = 100;
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
