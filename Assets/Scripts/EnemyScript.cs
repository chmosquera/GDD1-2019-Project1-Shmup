using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    private WeaponScript[] _weapons;

    void Awake() {
        _weapons = GetComponentsInChildren<WeaponScript>();
    }

    
    // Update is called once per frame
    void Update()
    {
        foreach (WeaponScript weapon in _weapons) {
            if (weapon != null && weapon.CanAttack) {
                weapon.Attack(true);
            }
        }
        
    }
}
