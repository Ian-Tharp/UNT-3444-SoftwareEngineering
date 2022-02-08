using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int Health = 0;
    public int Damage = 0;
    public int EnemyType = 0;
    public int EnemyValue = 0;
    int TypeSelector = 0;

    // Start is called before the first frame update
    void Start() {
        //SetEnemyType();

    }

    //Change enemy type function
    void SetEnemyType(int TypeSelector) {
        
        //switch case here for determining health, damage, and currency based on type
        //balancing values to be determined
        switch (TypeSelector) {
            case 1:
                Health = 1;
                Damage = 1;
                EnemyType = 1;
                EnemyValue = 1;
                break;
            case 2:
                Health = 3;
                Damage = 2;
                EnemyType = 2;
                EnemyValue = 2;
                break;
            case 3:
                Health = 5;
                Damage = 3;
                EnemyType = 3;
                EnemyValue = 3;
                break;
            case 4:
                Health = 8;
                Damage = 5;
                EnemyType = 4;
                EnemyValue = 5;
                break;
            case 5:
                Health = 14;
                Damage = 8;
                EnemyType = 5;
                EnemyValue = 8;
                break;
            default:
                Health = 1;
                Damage = 1;
                EnemyType = 1;
                EnemyValue = 1;
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
