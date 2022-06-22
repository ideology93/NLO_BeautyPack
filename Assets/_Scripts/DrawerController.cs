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
                            hit.transform.GetComponent<DrawerState>().pull.Play("closepush");
                        }
                    }
                }


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
