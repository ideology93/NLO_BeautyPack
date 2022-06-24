using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiMover : MonoBehaviour
{
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;
    [SerializeField] float heightOffset;
    [SerializeField] float speedOffset;
    public GameFlow flow;
    public Confetti confetti;

    // Update is called once per frame
    void Update()
    {
        if (flow.confettiPhase)
        {
            Vector3 v3;

            if (Input.touchCount != 1)
            {
                dragging = false;
                return;
            }

            Touch touch = Input.touches[0];
            Vector3 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Confetti")
                    {
                            toDrag = hit.transform;
                            dist = hit.transform.position.z;
                            v3 = new Vector3(pos.x, pos.y, dist);
                            v3 = Camera.main.ScreenToWorldPoint(v3);
                            offset = toDrag.position - v3;
                            dragging = true;
                            
                    }
                }
            }

            if (dragging && touch.phase == TouchPhase.Moved)
            {

                v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                if (toDrag.position.x < Screen.width / 3)
                {
                    toDrag.position = v3 + offset;
                }
                confetti.SpawnConfetti();
            }

            if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                dragging = false;
            }
        }
    }
}