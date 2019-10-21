using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{

    [Tooltip("Damage inflicted")]
    public int damage = 1;

    [Tooltip("Projectile damage player or enemies?")]
    public bool isEnemyShot = false;

    public bool isIce = false;
    
    void Start()
    {
        // the bullet gets destroyed after x seconds
        Destroy(gameObject, 20);
    }

    
    void Update()
    {
        
    }
}
