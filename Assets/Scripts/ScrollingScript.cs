using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(2,2);

    public Vector2 direction = new Vector2(-1, 0);

    public bool isLinkedToCamera = false;

    public bool isLooping = false;

    private List<SpriteRenderer> backgroundPart;

    void Start() {
        if (isLooping) {
            backgroundPart = new List<SpriteRenderer>();

            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                SpriteRenderer r = child.GetComponent<SpriteRenderer>();

                if (r != null) {
                    backgroundPart.Add(r);
                }
            }

            // Sort by position
            backgroundPart = backgroundPart.OrderBy(
              t => t.transform.position.x
            ).ToList();
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector3 movement = new Vector3(
            speed.x * direction.x,
            speed.y * direction.y,
            0
        );

        movement *= Time.deltaTime;
        transform.Translate(movement);

        // Move the camera
        if (isLinkedToCamera) {
            Camera.main.transform.Translate(movement);
        }

        if (isLooping) {
            // Get the first object.
            // The list is ordered from left (x position) to right.
            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null) {
                    
                // Check if the child is already (partly) before the camera.
                // We test the position first because the IsVisibleFrom
                // method is a bit heavier to execute.
                if (firstChild.transform.position.x < Camera.main.transform.position.x)
                {
                    // If the child is already on the left of the camera,
                    // we test if it's completely outside and needs to be
                    // recycled.
                    if (firstChild.isVisibleFrom(Camera.main) == false) {
                        
                        // Get the last child position
                        SpriteRenderer lastChild = backgroundPart.LastOrDefault();

                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

                        // Set the position of the recycled one to be after the last child
                        firstChild.transform.position = new Vector3(
                            lastPosition.x + lastSize.x, 
                            firstChild.transform.position.y,
                            firstChild.transform.position.z
                        );

                        // Set the recycled child to be the last position of the backgroundParts list
                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
    }
}
