using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Tooltip("Projectile prefab for shooting")]
    public Transform shotPrefab;

    [Tooltip("Cooldown in seconds between two shots")]
    public float shootingRate = 0.25f;

    private float _shootCooldown;

    public Sprite icon;

    
    public bool CanAttack {
        get {
            return _shootCooldown <= 0f;
        }
    }

    void Start()
    {
      _shootCooldown = 0f;  
    }

    
    void Update()
    {
        if (_shootCooldown > 0) {
            _shootCooldown -= Time.deltaTime;
        }
    }

    public ShotScript Attack(bool isEnemy) {
        
        if (CanAttack) {
            _shootCooldown = shootingRate;
            Transform shotTransform = Instantiate(shotPrefab) as Transform;

            shotTransform.position = transform.position;

            // Specify if shot belongs to enemy or not
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null) {
                shot.isEnemyShot = isEnemy;
            }

            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null) {
                move.direction = this.transform.right;
            }

            return shot;
        }

        return null;
    }

}
