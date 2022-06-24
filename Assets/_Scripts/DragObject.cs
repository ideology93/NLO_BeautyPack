using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using DG.Tweening;



public class DragObject : MonoBehaviour

{

    private Vector3 mOffset;
    private float mZCoord;
    public Confetti confetti;
    public bool isConfetti;
    private bool isHeld;
    public Transform target;
    private GameFlow flow;
    public SelectObject so;
    public AudioManager am;
    void Start()
    {
        flow = FindObjectOfType<GameManager>().GetComponent<GameFlow>();
        so = FindObjectOfType<GameManager>().GetComponent<SelectObject>();
        am = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }
    void OnMouseDown()
    {

        mZCoord = Camera.main.WorldToScreenPoint(
            gameObject.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Confetti")
            {
                isConfetti = true;
                target = hit.transform;
            }
            else
            {
                isConfetti = false;
            }

            if (hit.collider.tag == "Beauty" || hit.collider.tag == "Card")
            {
                print("At targetting");
                target = hit.transform;
            }
            isHeld = true;

        }

    }

    private Vector3 GetMouseAsWorldPoint()

    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDrag()
    {
        if (isConfetti && !flow.allowDrag)
        {
            transform.position = new Vector3(GetMouseAsWorldPoint().x + mOffset.x, target.position.y, GetMouseAsWorldPoint().z + mOffset.z);

            confetti.SpawnConfetti();

        }
        if (flow.allowDrag)
        {
            print("At moving non confetti");
            transform.position = new Vector3(GetMouseAsWorldPoint().x + mOffset.x, target.position.y, GetMouseAsWorldPoint().z + mOffset.z);

        }

    }
    void OnMouseUp()
    {

        // if (transform.position.x <= -0.22 && transform.position.x > = 0.235)
        // {
        //     print("inside z");
        //     if(transform.position.z >= -0.135 && transform.position.z <= 0.125){

        //         print("inside x");
        //     }
        //     transform.Translate(transform.position.x,transform.position.y-1,transform.position.y, Space.World);
        // }

        isHeld = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (!isHeld)
        {
            if (other.tag == "Inbox" && (gameObject.tag == "Beauty" || gameObject.tag == "Card"))
            {
                print("At in trigger");
                if (gameObject.tag == "Beauty")
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
                else if(gameObject.tag=="Card")
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                if (gameObject.tag != "Card")
                {
                    transform.GetComponent<ProductSelected>().isAdded = true;
                }
                transform.SetParent(GameObject.Find("ProductsInBox").transform);
                so.CheckBox();
                if (gameObject.tag == "Card")
                {
                    flow.EndCardPhase();
                    Destroy(gameObject, 1.4f);
                }

            }
        }
    }

}