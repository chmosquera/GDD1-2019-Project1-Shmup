using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private bool hasSpawned;
    private WeaponScript[] weapons;
    private MoveScript moveScript;
    private Collider2D colliderComponent;
    private SpriteRenderer rendererComponent;

    public List<ShotScript> activeShots = new List<ShotScript>();

    void Awake() {
        weapons = GetComponentsInChildren<WeaponScript>();
        moveScript = GetComponent<MoveScript>();
        colliderComponent = GetComponent<Collider2D>();
        rendererComponent = GetComponent<SpriteRenderer>();

    }

    void Start() {

        hasSpawned = false;

        // Disable everything
        moveScript.enabled = false;
        colliderComponent.enabled = false;
        
        foreach(WeaponScript weapon in weapons) {
            weapon.enabled = false;
        }
    }

    void Update()
    {

        // Check if enemy has spawned
        if (hasSpawned == false) {

            // Entering camera view? Spawn the enemy.
            if (rendererComponent.isVisibleFrom(Camera.main)) {
                Spawn();
            } 
        }
        else {

            // Clean list - Remove inactive shots
            activeShots.RemoveAll(item => item == null);

            // Auto-fire
            foreach (WeaponScript weapon in weapons) {
                if (weapon != null && weapon.enabled && weapon.CanAttack) {
                    ShotScript shot = weapon.Attack(true);
                    activeShots.Add(shot);
                }
            }

            // Out of the camera? destroy the game object
            if (rendererComponent.isVisibleFrom(Camera.main) == false) {
                Destroy(this.gameObject);
            }

        }
        
        
    }

    private void Spawn() {
        hasSpawned = true;

        colliderComponent.enabled = true;
        moveScript.enabled = true;
        foreach (WeaponScript weapon in weapons) {
            weapon.enabled = true;
        }
    }

    void OnDestroy() {

        // Destroy enemy and ALL shots fired
        Debug.Log(this.name  + " has been destroyed.");
        foreach (ShotScript shot in activeShots) {
            if (shot != null) Destroy(shot.gameObject);
        }

        // Update score
        ScoreScript.instance.score++;
       
    }
}
