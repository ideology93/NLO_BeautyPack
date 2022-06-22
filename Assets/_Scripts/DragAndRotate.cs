using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class DragAndRotate : MonoBehaviour
{
    public bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            
            if (Input.touchCount == 1)
            {
                Touch screenTouch = Input.GetTouch(0);
 
                if (screenTouch.phase == TouchPhase.Moved)
                {
                    transform.parent.Rotate(0f, screenTouch.deltaPosition.x, 0f);
                }
 
                if (screenTouch.phase == TouchPhase.Ended)
                {
                    isActive = false;
                }
            }
        }
    }
    
}