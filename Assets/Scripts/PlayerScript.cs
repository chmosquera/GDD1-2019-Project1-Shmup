using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Speed of the ship")]
    public Vector2 speed = new Vector2(50,50);

    //[Header("Ship's movement and component")]
    private Vector2 _movement;
    private Rigidbody2D _rbComponent;

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // movement per direction
        _movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY
        );

        // Shooting
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");

        if (shoot) {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null) {

                weapon.Attack(false);
            }
        }

    }

    void FixedUpdate() {
        if (_rbComponent == null) {
            _rbComponent = GetComponent<Rigidbody2D>();
        }

        _rbComponent.velocity = _movement;
    }

    void OnCollisionEnter2D(Collision2D other) {
        // handle collisions with enemy
        EnemyScript enemy = other.gameObject.GetComponent<EnemyScript>();
        if (enemy != null) {

            // destroy the enemy completely
            HealthScript enemy_health = enemy.GetComponent<HealthScript>();
            if (enemy_health != null) {
                enemy_health.Damage(enemy_health.hp);
            }

            // but we also take 1 damage
            HealthScript my_health = GetComponent<HealthScript>();
            if (my_health != null) {
                my_health.Damage(1);
            }
        }
    }

}
