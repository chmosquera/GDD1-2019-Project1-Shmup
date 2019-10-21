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

    
    // Update is called once per frame
    void Update()
    {

        if (hasSpawned == false) {
            if (rendererComponent.isVisibleFrom(Camera.main)) {
                Spawn();
            } else {

                foreach (WeaponScript weapon in weapons) {
                    if (weapon != null && weapon.enabled && weapon.CanAttack) {
                        weapon.Attack(true);
                    }
                }

                // Out of the camera? destroy the game object
                if (rendererComponent.isVisibleFrom(Camera.main) == false) {
                    Destroy(this.gameObject);
                }

            }
        }

        foreach (WeaponScript weapon in weapons) {
            if (weapon != null && weapon.CanAttack) {
                weapon.Attack(true);
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
}
