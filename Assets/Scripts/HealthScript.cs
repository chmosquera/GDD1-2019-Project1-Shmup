using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    [Tooltip("total hitpoints")]
    public int hp = 1;

    [Tooltip("Is player or enemy?")]
    public bool isEnemy = true;

    /// <summary> 
    /// Damages the health by x amount of points  
    /// </summary>
    public void Damage(int damageCount) {
        hp -= damageCount;

        if (hp <= 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        ShotScript shot = other.gameObject.GetComponent<ShotScript>();
        if (shot != null) {
            
            // Avoid friendly fire
            if (shot.isEnemyShot != isEnemy) {

                Damage(shot.damage);

                // Destroy the shot
                Destroy(shot.gameObject);
            }
        }

    }

}
