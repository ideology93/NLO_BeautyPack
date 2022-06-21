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
                    Test(hit);
                    if (!isOpen)
                    {


                        if (hit.transform.tag == "Drawer" && !EventSystem.current.IsPointerOverGameObject())
                        {
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
            isOpen = true;
            pull.Play("openpull");
            open = true;
            yield return new WaitForSeconds(.5f);
        }
    }

    public IEnumerator closing()
    {
        isOpen = false;
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
        isOpen=false;
        if (flow.hasStarted)
        {
            {
                
                if (!hit.transform.GetComponent<Drawer_Pull_Z>().open && !hit.transform.GetComponent<Drawer_Pull_Z>().open)
                {
                    if (Input.GetMouseButtonDown(0))
                    {

                        StartCoroutine(hit.transform.GetComponent<Drawer_Pull_Z>().opening());
                        
                    }
                }
                else
                {
                    if (hit.transform.GetComponent<Drawer_Pull_Z>().open && !hit.transform.GetComponent<Drawer_Pull_Z>().open)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            StartCoroutine(hit.transform.GetComponent<Drawer_Pull_Z>().closing());
                        }
                    }

                }

            }
        }
    }

}
