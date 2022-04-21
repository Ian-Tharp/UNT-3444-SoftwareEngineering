using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerStats : MonoBehaviour
{
    public long Score = 0;
    public int TotalHealth = 100;
    public int CurrentHealth = 0;
    public int CurrentShields = 0;
    public int TotalShields = 0;
    public int regenAmount = 0;

    public bool hasShield = false;
    public bool isDead = false;

    public float bloodSpilled;
    public int explosions;
    public int killed;
    public int eliteKilled;
    public int shots;

    //--------------------------------------------------------------------------------
    //Additions
    public void AddScore(int Amount) {
        Score += Amount;

    }

    public void AddCurrentHealth(int Amount) {
        CurrentHealth += Amount;
        //Addition overflow check
        if (CurrentHealth > TotalHealth) {
            CurrentHealth = TotalHealth;
        }
    }

    public void AddRegenAmount(int Amount) {
        regenAmount += Amount; 
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

    public void SubtractCurrentShields(int Amount) {
        if (CurrentShields - Amount >= 0) {
            CurrentShields -= Amount;
        }
        else {
            int temp = Mathf.Abs(CurrentShields - Amount);
            SubtractCurrentHealth(temp);
            hasShield = false;
        }
    }

    public void ResetHealth() { CurrentHealth = TotalHealth; }
    public void ResetScore() { Score = 0; }
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

    public PlayerInput playerControls;
    private InputAction KillAllEnemies;
    //Constructor
    void Start() {
        CurrentHealth = TotalHealth;
        playerControls = new PlayerInput();
        isDead = false;

        bloodSpilled = 0;
        explosions = 0;
        killed = 0;
        shots = 0;
    }

    //Called once every frame
    void Update() {
        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            isDead = true;
        }
        
        //Checks if player isDead bool is true
        //If so, quit the game for now
        //We will implement death screen functionality later
        if (isDead) {
            
            //Application.Quit();
        }
    }
}
