using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject instruction;
    public float time = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(instruction, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
