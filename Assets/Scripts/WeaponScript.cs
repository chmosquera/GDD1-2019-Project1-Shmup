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

    public void Attack(bool isEnemy) {
        
        if (CanAttack) {
            _shootCooldown = shootingRate;
            var shotTransform = Instantiate(shotPrefab) as Transform;

            shotTransform.position = transform.position;

            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null) {
                shot.isEnemyShot = isEnemy;
            }

            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null) {
                move.direction = this.transform.right;
            }
        }
    }

}
