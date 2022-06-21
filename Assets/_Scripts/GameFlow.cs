using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private AudioManager am;
    [Header("Animators")]
    [SerializeField] private Animator boxAnimate;
    [SerializeField] private Animator presentAnimate;
    public Animator questAnimate;
    public Animator phoneAnimate;
    [Header("UI's & GameObjects")]
    public GameObject topQuestMenu;
    public GameObject questBubble;
    public GameObject overlayUI;
    public GameObject youFailedUI;
    public GameObject startButton;
    public GameObject present;
    public GameObject endUI;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject sendUI;
    public GameObject mainUI;
    public GameObject bottomMenu;
    public GameObject orderDialogue;
    public GameObject spritesObject;
    public Sprite questionMark;
    public CameraPositions cams;
    [Header("Lists")]
    public List<Sprite> expectedOrder = new List<Sprite>();
    public List<string> currentOrder = new List<string>();
    public List<Sprite> allSprites = new List<Sprite>();
    [Header("Scripts etc")]
    public PlayerStats ps;
    public Stars stars;
    public Present presentScript;
    [Header("Checkers")]
    int orders = 0;
    private float timer;
    public bool isOpen;
    
    public GameObject[] drawers;

    int[] randoms = new int[3];
    [HideInInspector]
    public bool isPickOver, isPackOver, isAdded, isDone, hasStarted;

    void Awake()
    {
        isPickOver = false;
        isPackOver = false;
        isAdded = false;
        isDone = false;
        hasStarted = false;
        phoneAnimate.Play("SmartphoneMoveIn");
        GenerateQuest();
        // fill list of expected orders 
        foreach (Transform t in orderDialogue.transform)
        {
            expectedOrder.Add(t.GetComponent<Image>().sprite);
        }
        expectedOrder = expectedOrder.OrderBy(x => x.name).ToList();

        for (int i = 0; i < expectedOrder.Count; i++)
        {

            Debug.Log("Expected " + expectedOrder[i].name);
        }
        // add all sprites to list
        foreach (Transform s in spritesObject.transform)
        {
            allSprites.Add(s.GetComponent<SpriteRenderer>().sprite);
        }
        //FillBottomMenu();



    }

    public void GenerateQuest()
    {

        for (int i = 0; i < 3; i++)
        {
            randoms[i] = Random.Range(1, spritesObject.transform.childCount - 1);
        }


        for (int i = 0; i <= 2; i++)
        {
            orderDialogue.transform.GetChild(i).GetComponent<Image>().sprite = spritesObject.transform.GetChild(randoms[i]).GetComponent<SpriteRenderer>().sprite;
            orderDialogue.transform.GetChild(i).GetComponent<Image>().SetNativeSize();
        }
        for (int i = 0; i <= 2; i++)
        {
            topQuestMenu.transform.GetChild(i).GetComponent<Image>().sprite = orderDialogue.transform.GetChild(i).GetComponent<Image>().sprite;
        }

    }


    public void StartLevel()
    {

        questAnimate.SetBool("isQuestActive", true);
        Toggle(startButton);
        Toggle(bottomMenu);
        
        hasStarted = true;
        phoneAnimate.Play("SmartphoneMoveOut");
        topQuestMenu.SetActive(true);
    }   
    public void EndPickPhase()
    {


        Toggle(bottomMenu);
        isDone = true;
        hasStarted = false;
        isPickOver = true;
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);

    }
    public void StartPackPhase()
    {
        if (isPickOver)
        {
            boxAnimate.Play("PresentFadeIn");
            phoneAnimate.Play("SmartphoneMoveOut");
        }
    }
    public void EndPackPhase()
    {
        if (isPackOver)
            sendUI.SetActive(true);
    }
    public void Send()
    {

        ps.AddCoins();
        sendUI.SetActive(false);
        StarsAchieved();

    }

    public int CheckOrder()
    {
        //check how many of the orders are correct for scoring system

        currentOrder = currentOrder.OrderBy(x => x).ToList();
        for (int i = 0; i < currentOrder.Count; i++)
        {
            Debug.Log("Current order: " + currentOrder[i]);
        }

        for (int i = 0; i < currentOrder.Count; i++)
        {
            if (currentOrder.Contains(expectedOrder[i].name))
            {
                orders++;
            }
        }
        isPackOver = true;
        int total = (orders * 50);

        Debug.Log(total);
        return total;
    }


    public int CalculateCoins()
    {
        int coins = 0;
        if (isPackOver)
        {
            Debug.Log("Got to checking order");
            coins = CheckOrder();
        }
        return coins;

    }

    public void StarsAchieved()
    {

        Debug.Log("Correct orders:" + orders);
        if (orders == 0)
        {
            Debug.Log("No star");
            Toggle(youFailedUI);
            am.PlayClip(3);
            return;
        }
        else if (orders == 1) // 1 && orders < 3)
        {
            Debug.Log("One star");
            Toggle(endUI);

            stars.StarsScore(1);
        }
        else if (orders == 2) // && orders < 5)
        {
            Debug.Log("Two stars");
            Toggle(endUI);

            stars.StarsScore(2);
        }
        else
        {
            Debug.Log("Three stars");
            Toggle(endUI);

            stars.StarsScore(3);
        }
    }
    public void Toggle(GameObject ui)
    {

        if (ui.activeSelf)
        {
            ui.SetActive(false);
        }
        else
        {
            ui.SetActive(true);
        }
    }



}
