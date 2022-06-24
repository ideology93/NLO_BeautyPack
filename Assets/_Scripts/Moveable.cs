using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    private bool isHeld;
    private float speed = 0.01f;
    private Transform target;

    void Update()
    {
        if (Input.touchCount != 1)
        {
            isHeld = false;
            return;
        }
        Touch touch = Input.touches[0];
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Beauty")
                {
                    isHeld = true;
                    target = hit.transform;
                }

            }
        }
        if (touch.phase == TouchPhase.Moved)
        {
            if (isHeld) 
            {
                target.position = new Vector3(target.position.x + touch.deltaPosition.y *speed,
                target.position.y, target.position.z + touch.deltaPosition.x*speed );
            }
        }
        if (touch.phase == TouchPhase.Ended)
        {
            isHeld = false;
        }

    }
}
