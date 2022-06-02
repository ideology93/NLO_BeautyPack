using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;


public class Drawer_Pull_Z : MonoBehaviour
{

    public Animator pull;
    public bool open;
    public GameFlow flow;


    void Start()
    {

        open = false;
    }

    void OnMouseDown()
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


}
