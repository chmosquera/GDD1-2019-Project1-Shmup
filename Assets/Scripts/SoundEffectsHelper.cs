using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsHelper : MonoBehaviour
{

    public static SoundEffectsHelper instance;

    public AudioClip explosionSound;
    public AudioClip playerShotSound;

    public AudioClip enemyShotSound;

    void Awake() {
        if (instance != null) return;
        else instance = this;
    }

    public void MakeExplosionSound()
    {
        MakeSound(explosionSound);
    }

    public void MakePlayerShotSound()
    {
        MakeSound(playerShotSound);
    }

    public void MakeEnemyShotSound()
    {
        MakeSound(enemyShotSound);
    }    

    private void MakeSound(AudioClip originalClip) {
            AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }

}
