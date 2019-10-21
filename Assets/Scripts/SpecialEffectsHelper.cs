using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
    public static SpecialEffectsHelper instance;

    public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;

    void Awake() {
        if (instance != null) return;
        else instance = this;
    }

    public void Explosion(Vector3 position) {
        instantiate(smokeEffect, position);
        instantiate(fireEffect, position);
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position) {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;

        //Destroy(newParticleSystem.gameObject, newParticleSystem.startLifetime);

        return newParticleSystem;
        
    }
}
