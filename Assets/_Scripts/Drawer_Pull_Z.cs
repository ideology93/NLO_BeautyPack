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
                    if (hit.transform.tag == "Drawer" && !EventSystem.current.IsPointerOverGameObject())
                    {
                        Test(hit);
                        camPos = 1;
                        drawer = int.Parse(hit.transform.name);
                        Debug.Log(drawer);
                        if (hit.transform.GetComponent<Drawer_Pull_Z>().open)
                            cams.MoveCamera(drawer);

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
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            flow.isOpen = true;
            pull.Play("openpull");
            open = true;
            yield return new WaitForSeconds(.5f);
        }
    }

    public IEnumerator closing()
    {
        flow.isOpen = false;
        pull.Play("closepush");
        open = false;
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
    void Test(RaycastHit hit)
    {
        Debug.Log("ColliderName" + hit.transform.name);
        if (flow.hasStarted)
        {
            {

                if (!hit.transform.GetComponent<Drawer_Pull_Z>().open && !flow.isOpen)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        CollidersOff(int.Parse(hit.transform.name));
                        StartCoroutine(hit.transform.GetComponent<Drawer_Pull_Z>().opening());

                    }
                }
                else
                {
                    if (hit.transform.GetComponent<Drawer_Pull_Z>().open)
                    {

                        if (Input.GetMouseButtonDown(0))
                        {
                            CollidersOn();
                            StartCoroutine(hit.transform.GetComponent<Drawer_Pull_Z>().closing());
                        }
                    }

                }

            }
        }
    }
    void CollidersOff(int a)
    {
        Debug.Log("hit drawer is " +a);
        for (int i = 0; i < flow.drawers.Length; i++)
        {
            if (i != a)
            {
                Debug.Log("we turned off" + i);
                flow.drawers[i].gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
    void CollidersOn()
    {
        for (int i = 0; i < flow.drawers.Length; i++)
        {

           flow.drawers[i].gameObject.GetComponent<BoxCollider>().enabled = true;

        }
    }


}
