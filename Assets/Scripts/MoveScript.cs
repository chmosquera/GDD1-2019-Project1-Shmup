using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveScript : MonoBehaviour
{
    public Vector3 speed = new Vector2(10,10);
    public Vector2 direction = new Vector2(-1,0);

    private Vector2 _movement;
    private Rigidbody2D _rigidbodyComponent;

    void Update() {
        _movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y
        );
        
    }

    void FixedUpdate() {
        if (_rigidbodyComponent == null) {
            _rigidbodyComponent = GetComponent<Rigidbody2D>();
        }

        _rigidbodyComponent.velocity = _movement;
    }
}