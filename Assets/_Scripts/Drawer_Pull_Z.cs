using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;


public class Drawer_Pull_Z : MonoBehaviour
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

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("OH GOD PLEASE HELp");
                    if (hit.transform.tag == "Drawer")
                    {

                        camPos = 1;
                        drawer = int.Parse(hit.transform.name);
                        cams.MoveCamera(drawer);
                        if (!hit.transform.GetComponent<DrawerState>().isOpen && !flow.isOpen)
                        {
                            opening();
                            hit.transform.GetComponent<DrawerState>().isOpen = true;
                        }
                        else if (hit.transform.GetComponent<DrawerState>().isOpen)
                        {
                            closing();
                            hit.transform.GetComponent<DrawerState>().isOpen = false;
                        }
                    }
                }


            }
        }
    }
    // void OnMouseDown()
    // {
    //     isOpen = false;
    //     if (flow.hasStarted)
    //     {
    //         {
    //             if (!open && !isOpen)
    //             {
    //                 if (Input.GetMouseButtonDown(0))
    //                 {

    //                     StartCoroutine(opening());

    //                 }
    //             }
    //             else
    //             {
    //                 if (open && !isOpen)
    //                 {
    //                     if (Input.GetMouseButtonDown(0))
    //                     {
    //                         StartCoroutine(closing());
    //                     }
    //                 }

    //             }

    //         }
    //     }

    // }

    IEnumerator opening()
    {
        Debug.Log("opening");
        flow.isOpen = true;
        pull.Play("openpull");
        yield return new WaitForSeconds(.5f);

    }

    public IEnumerator closing()
    {
        Debug.Log("closing");
        DrawerChecker();
        pull.Play("closepush");
        yield return new WaitForSeconds(.5f);

    }
    // void DrawerController(RaycastHit hit)
    // {

    //     if (hit.collider.tag == "Drawer")
    //     {
    //         if (!hit.collider.GetComponent<Drawer_Pull_Z>().open)
    //         {
    //             if (Input.GetMouseButtonDown(0))
    //             {
    //                 Debug.Log("We got here");
    //                 StartCoroutine(opening());
    //             }
    //         }
    //         else
    //         {
    //             if (hit.collider.GetComponent<Drawer_Pull_Z>().open)
    //             {
    //                 if (Input.GetMouseButtonDown(0))
    //                 {
    //                     StartCoroutine(closing());
    //                 }
    //             }

    //         }

    //     }
    // }
    // void Test(RaycastHit hit)
    // {

    //     if (!hit.transform.GetComponent<Drawer_Pull_Z>().open)
    //     {

    //         Debug.Log("Is a drawer open - " + flow.isOpen);
    //         CollidersOff(int.Parse(hit.transform.name));
    //         StartCoroutine(hit.transform.GetComponent<Drawer_Pull_Z>().opening());
    //         flow.isOpen = true;


    //     }
    //     else
    //     {
    //         if (hit.transform.GetComponent<Drawer_Pull_Z>().open)
    //         {


    //             CollidersOn();
    //             StartCoroutine(hit.transform.GetComponent<Drawer_Pull_Z>().closing());
    //             flow.isOpen = false;
    //         }


    //     }



    // }
    void CollidersOff(int a)
    {
        Debug.Log("hit drawer is " + a);
        for (int i = 0; i < flow.drawers.Length; i++)
        {
            if (i != a)
            {
                Debug.Log("we turned off" + i);
                flow.drawers[i].enabled = false;
            }
        }
    }
    void CollidersOn()
    {
        for (int i = 0; i < flow.drawers.Length; i++)
        {

            flow.drawers[i].enabled = true;

        }
    }
    void DrawerChecker()
    {
        int tmp = 0;
        for (int i = 0; i < flow.drawers.Length; i++)
        {
            if (!flow.drawers[i].GetComponent<DrawerState>().isOpen)
            {
                tmp++;
            }
            if (tmp == flow.drawers.Length)
            {
                flow.isOpen = true;
            }
        }

    }


}
