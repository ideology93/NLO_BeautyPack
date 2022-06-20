using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;


public class Drawer_Pull_Z : MonoBehaviour
{

    public Animator pull;
    public bool open;
    public GameFlow flow;
    public int drawer;
    private CameraPositions cams;


    void Start()
    {
        cams = Camera.main.GetComponent<CameraPositions>();
        open = false;
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Drawer" && !EventSystem.current.IsPointerOverGameObject())
            {
                drawer = int.Parse(hit.transform.name);
                if (drawer <= 3)
                {
                    cams.MoveLeft();
                }       
                if (drawer > 3)
                {
                    cams.MoveRight();
                }
            }

        }
    }
    // void OnMouseDown()
    // {
    //     {
    //         if (!open)
    //         {
    //             if (Input.GetMouseButtonDown(0))
    //             {

    //                 StartCoroutine(opening());
    //             }
    //         }
    //         else
    //         {
    //             if (open)
    //             {
    //                 if (Input.GetMouseButtonDown(0))
    //                 {
    //                     StartCoroutine(closing());
    //                 }
    //             }

    //         }

    //     }

    // }

    IEnumerator opening()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {

            pull.Play("openpull");
            open = true;
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator closing()
    {

        pull.Play("closepush");
        open = false;
        yield return new WaitForSeconds(.5f);
    }
    void DrawerController()
    {
        {
            if (!open)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    StartCoroutine(opening());
                }
            }
            else
            {
                if (open)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(closing());
                    }
                }

            }

        }
    }


}
