using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    // Start is called before the first frame update

    void Update()
    {

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //if (Input.GetMouseButton(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("We're here");
                if (hit.transform.tag == "LazySusan")
                {
                    Debug.Log("Targeted Susan");
                    DragAndRotate objectScript = hit.collider.GetComponent<DragAndRotate>();
                    objectScript.isActive = !objectScript.isActive;
                }
            }
        }

    }
}