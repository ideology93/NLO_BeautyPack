using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Drawer_Pull_X : MonoBehaviour
{

    public Animator pull_01;
    public bool open;
    public Transform Player;

    void Start()
    {
        open = false;
    }

    public void OnMouseOver()
    {
        {

            Debug.Log(open);
            if (open == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(opening());
                }
            }
            else
            {
                if (open == true)
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
        
        pull_01.Play("openpull_01");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator closing()
    {
        
        pull_01.Play("closepush_01");
        open = false;
        yield return new WaitForSeconds(.5f);
    }


}
