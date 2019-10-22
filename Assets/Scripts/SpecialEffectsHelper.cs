using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
    public static SpecialEffectsHelper instance;

    public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;
    public ParticleSystem laserEffect;

    void Awake() {
        if (instance != null) return;
        else instance = this;
    }

    public void Explosion(Vector3 position) {
        instantiate(smokeEffect, position);
        instantiate(fireEffect, position);
    }

    public void Laser(Vector3 position) {
        ParticleSystem particle = instantiate(laserEffect, position);

    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position) {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;

        //Destroy(newParticleSystem.gameObject, newParticleSystem.startLifetime);

        return newParticleSystem;
    }
}
