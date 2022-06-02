using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Shop : MonoBehaviour
{
    public GameObject tabPresent0, tabTable0;
    [Header("Parts to color")]
    public GameObject[] parts;
    public GameObject[] parts2;
    public GameObject tableTop;
    public GameObject tabPresent;
    public GameObject tabTable;
    public static bool presentbool, tablebool;
    [Header("Sprites")]
    public Sprite present;
    public Sprite table;
    [Header("Materials and colors")]
    Material tempMat;
    Color prevColor, newColor;
    [Header("Scripts")]
    public Present presentClass;
    private PlayerStats ps;
    private void Start()
    {
        ps = gameObject.GetComponent<PlayerStats>();
        tempMat = tableTop.GetComponent<Renderer>().material;
        prevColor = tempMat.color;
        newColor = Color.yellow;
        if (PlayerPrefs.GetInt("present") == 1)
        {
            GameObject checkmark = GameObject.Find("Check");
            Debug.Log("We're in present");
            tabPresent.GetComponent<Image>().sprite = present;
            tabPresent.transform.GetChild(1).gameObject.SetActive(false);
            if (presentbool)
            {
                presentClass.ChangePresentColor(1);
            }
            else if(!presentbool)
                presentClass.ChangePresentColor(2);


        }
        if (PlayerPrefs.GetInt("table") == 1)
        {
            Debug.Log("We're in table");
            tabTable.GetComponent<Image>().sprite = table; ;
            tabTable.transform.GetChild(1).gameObject.SetActive(false);
            if (tablebool)
            {
                TableColor(1);
            }
            else if(!tablebool)
                TableColor(2);

        }

    }


    public void Click()
    {

        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        if (clicked.GetComponent<Image>().sprite.name == "GUI_0")
        {

            clicked.GetComponent<Image>().sprite = present;
            //checkmark.transform.SetParent(clicked.transform, false);
            //checkmark.transform.position = new Vector3(clicked.transform.position.x, checkmark.transform.position.y, clicked.transform.position.z + 1);
            clicked.transform.GetChild(1).gameObject.SetActive(false);
            PlayerPrefs.SetInt("present", 1);

        }
        else
        {
            // checkmark.transform.SetParent(clicked.transform, false);
            //checkmark.transform.position = new Vector3(clicked.transform.position.x, checkmark.transform.position.y, clicked.transform.position.z + 1);
        }

    }
    public void ClickTable()
    {

        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        if (clicked.GetComponent<Image>().sprite.name == "GUI_0")
        {
            clicked.GetComponent<Image>().sprite = table;
            //checkmark.transform.SetParent(clicked.transform, false);
            //checkmark.transform.position = new Vector3(clicked.transform.position.x, checkmark.transform.position.y, clicked.transform.position.z + 1);
            clicked.transform.GetChild(1).gameObject.SetActive(false);

            PlayerPrefs.SetInt("table", 1);
        }
        else
        {
            //checkmark.transform.SetParent(clicked.transform, false);
            //checkmark.transform.position = new Vector3(clicked.transform.position.x, checkmark.transform.position.y, clicked.transform.position.z + 1);
        }


    }
    public void TableColor(int a)
    {
        if (a == 1)
        {
            tableTop.GetComponent<Renderer>().material.color = prevColor;

        }
        if (a == 2)
        {
            tableTop.GetComponent<Renderer>().material.color = newColor;
        }
    }
    public void activePresent1()
    {
        EventSystem.current.currentSelectedGameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
        tabPresent0.transform.GetChild(0).transform.gameObject.SetActive(false);
        presentbool = false;

    }
    public void activeTable1()
    {
        EventSystem.current.currentSelectedGameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
        tabTable0.transform.GetChild(0).transform.gameObject.SetActive(false);
        tablebool = false;

    }
    public void activePresent0()
    {
        EventSystem.current.currentSelectedGameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
        tabPresent.transform.GetChild(0).transform.gameObject.SetActive(false);
        presentbool = true;

    }
    public void activeTable0()
    {
        EventSystem.current.currentSelectedGameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
        tabTable.transform.GetChild(0).transform.gameObject.SetActive(false);
        tablebool = true;

    }




}

