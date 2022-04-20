using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    private EnemyStats enemy;
    
    void Start() {
        
    }

    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Enemy") {
            
            
        }
    }
}
