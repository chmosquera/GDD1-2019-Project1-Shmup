using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject root;
    public GameObject spawnObject;
    public float rate;
    public float startDelay = 0f;

    public bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {     
    }

    void Update() {

        if (hasSpawned == false) {
            // Begin spawning after the delay
            if (startDelay <= 0) {
                StartCoroutine(Spawn());
            } else {
                startDelay -= Time.deltaTime;
            }
        }
    }

    
    IEnumerator Spawn() {
        Debug.Log("Spawning object");
        hasSpawned = true;
        // Spawn new object at spawner's location and store reference under root gameobject
        while (true) {
            GameObject newObject = Instantiate(spawnObject, root.transform);
            newObject.transform.position = this.transform.position;

            yield return new WaitForSeconds(1f/rate);
        }
    }

}
