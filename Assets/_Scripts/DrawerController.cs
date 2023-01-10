using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;


public class DrawerController : MonoBehaviour
{

    public Animator pull;
    public bool open;
    private GameFlow flow;
    public int drawer;
    private CameraPositions cams;
    public GameManager gm;
    public bool isOpen;
    public int camPos;
    public Drawer_Pull_Z state;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;



    void Start()
    {
        flow = FindObjectOfType<GameManager>().GetComponent<GameFlow>();
        cams = Camera.main.GetComponent<CameraPositions>();
        open = false;
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (flow.hasStarted)
        {

            if (Input.GetMouseButtonUp(0))
            {
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.tag == "Drawer")
                    {


                        if (!hit.transform.GetComponent<DrawerState>().isOpen && !flow.isOpen)
                        {
                            camPos = 1;
                            drawer = int.Parse(hit.transform.name);
                            cams.MoveCamera(drawer);
                            hit.transform.GetComponent<DrawerState>().isOpen = true;
                            StartCoroutine(opening());
                            hit.transform.GetComponent<DrawerState>().pull.Play("openpull");
                        }
                        else if (hit.transform.GetComponent<DrawerState>().isOpen)
                        {

                            hit.transform.GetComponent<DrawerState>().isOpen = false;
                            StartCoroutine(closing());
                            cams.MoveToTable();
                            hit.transform.GetComponent<DrawerState>().pull.Play("closepush");
                        }
                    }
                }



            }
        }
    }
    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards

            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("left swipe");
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("right swipe");
            }
        }
    }

    IEnumerator opening()
    {
        Debug.Log("opening");
        flow.isOpen = true;
        yield return new WaitForSeconds(.5f);

    }

    public IEnumerator closing()
    {
        Debug.Log("closing");
        DrawerChecker();
        yield return new WaitForSeconds(.5f);

    }


    void DrawerChecker()
    {
        int tmp = 0;
        for (int i = 0; i < flow.drawers.Length; i++)
        {

            if (!flow.drawers[i].GetComponent<DrawerState>().isOpen)
            {
                Debug.Log(tmp);
                tmp++;
            }
            if (tmp == flow.drawers.Length - 1)
            {
                flow.isOpen = false;
            }
        }

    }


}
