using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using DG.Tweening;
public class Present : MonoBehaviour
{
    public GameObject particle;
    public GameFlow flow;
    public Material matNew_1;
    public Material matNew_2;
    public Material matOG_1;
    public Material matOG_2;
    public bool isChanged;
    public List<Transform> boxesToRend = new List<Transform>();
    public List<Transform> sidesToRend = new List<Transform>();


    public void ChangePresentColor(int a)
    {
        if (a == 2)
        {
            foreach (Transform t in boxesToRend)
            {
                t.GetComponent<Renderer>().material = matNew_1;
            }
            foreach (Transform t in sidesToRend)
            {
                t.GetComponent<Renderer>().material = matNew_2;
            }

        }
        else if (a == 1)
        {
            foreach (Transform t in boxesToRend)
            {
                t.GetComponent<Renderer>().material = matOG_1;
            }
            foreach (Transform t in sidesToRend)
            {
                t.GetComponent<Renderer>().material = matOG_2;
            }

        }
    }

}
