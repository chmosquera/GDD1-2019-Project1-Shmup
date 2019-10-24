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

    [Header("Weapons")]
    public List<WeaponScript> weapons = new List<WeaponScript>();

    private int weaponIndex;
    public SpriteRenderer weaponRenderer;

    public float weaponChangeRate = 1f;
    private float weaponChangeCountDown = 0f;



    public WeaponScript currentWeapon {
        get {
            if (weaponIndex >= 0 || weaponIndex < weapons.Count) {
                return weapons[weaponIndex];
            } else return null;
        }
    }

    void Start() {

        weaponIndex = 0;

        foreach (WeaponScript weapon in this.GetComponentsInChildren<WeaponScript>()) {
            weapons.Add(weapon);
        }

        if (currentWeapon != null && weaponRenderer != null) {
            weaponRenderer.sprite = currentWeapon.icon;
        }
    }


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
        shoot |= Input.GetKeyDown(KeyCode.Space);

        if (shoot) {
            if (currentWeapon != null) {
                currentWeapon.Attack(false);
            }
        
        }

    }

    void FixedUpdate() {

        // Movement
        if (_rbComponent == null) {
            _rbComponent = GetComponent<Rigidbody2D>();
        }

        _rbComponent.velocity = _movement;


        // ----------------
        // Weapon Handling
        // ----------------
        // Next weapon
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && weaponChangeCountDown < 0) {
            if (weaponIndex < (weapons.Count - 1)) {
                weaponIndex++;
                weaponChangeCountDown = weaponChangeRate;
                Debug.Log("next weapon: " + currentWeapon.name);
            }
        }

        // Previous Weapon
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && weaponChangeCountDown < 0) {
            if (weaponIndex > 0) {
                weaponIndex--;
                weaponChangeCountDown = weaponChangeRate;
                Debug.Log("previous weapon: " + currentWeapon.name);
            }
        }

        // Update weapon icon
        weaponRenderer.sprite = currentWeapon.icon;

        weaponChangeCountDown -= Time.deltaTime;
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
                SoundEffectsHelper.instance.MakePlayerShotSound();
            }
        }
    }

    void OnDestroy() {
        var gameOver = FindObjectOfType<GameOverScript>();
        gameOver.ShowButtons();
    }
}
