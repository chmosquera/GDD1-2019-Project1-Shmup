﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundToViewport : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // 6 - Make sure we are not outside the camera bounds
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
        new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
        new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
        new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
        new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
        Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
        transform.position.z
        );
    }
}
