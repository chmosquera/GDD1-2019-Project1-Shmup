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
        SoundEffectsHelper.instance.MakeEnemyShotSound();

        if (hp <= 0) {
            SpecialEffectsHelper.instance.Explosion(transform.position);
            SoundEffectsHelper.instance.MakeExplosionSound();
            Debug.Log(this.name + " is dead");
            Destroy(gameObject);
        }
    }

    IEnumerator Freeze(float seconds) {
        MoveScript movement = GetComponent<MoveScript>();

        // if there is a movement script, freeze the movement
        if (movement != null) {

            // Save the original speed
            Vector3 originalSpeed = movement.speed;

            // Freeze! Stop the movement
            movement.speed = new Vector3(0,0,0);

            // Wait
            yield return new WaitForSeconds(seconds);

            // Okay, unfreeze!
            movement.speed = originalSpeed;
        }

        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other) {

        ShotScript shot = other.gameObject.GetComponent<ShotScript>();
        if (shot != null) {
            
            // Avoid friendly fire
            if (shot.isEnemyShot != isEnemy) {

                if (shot.shotType == ShotType.Ice) {
                    StartCoroutine(Freeze(2f));
                } 
                
                Damage(shot.damage);
               

                // Destroy the shot
                Destroy(shot.gameObject);
            }
        }

    }

}
