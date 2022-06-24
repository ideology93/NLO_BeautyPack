using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using PaintIn3D;
using DG.Tweening;

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
    public GameObject doneButton;

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

    public Collider[] drawers;
    public GameObject confettiSpawner;
    private DragObject confettiEnabler;
    public GameObject stickersUI;
    public GameObject stickersObject;
    public GameObject stickerUI_Image;
    public GameObject card;
    public GameObject P3DManager;
    public GameObject[] objectsToKill;

    int[] randoms = new int[3];
    [HideInInspector]
    public bool isPickOver, confettiPhase, confettiPhase2, isInAddingPhase, isPackOver, isAdded, isDone, hasStarted;
    public bool allowDrag;

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

        foreach (Transform s in stickersObject.transform)
        {
            GameObject obj = stickerUI_Image;
            Instantiate(obj, obj.transform.position, Quaternion.identity, stickersUI.transform);
            obj.transform.GetComponent<Image>().sprite = s.GetComponent<SpriteRenderer>().sprite;

            obj.SetActive(true);
        }


    }
    void Start()
    {
        confettiEnabler = confettiSpawner.GetComponent<DragObject>();
        P3DManager = GameObject.Find("P3DManager");
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
        hasStarted = true;
        questAnimate.SetBool("isQuestActive", true);
        Toggle(startButton);
        Toggle(bottomMenu);
        phoneAnimate.Play("SmartphoneMoveOut");
        topQuestMenu.SetActive(true);
    }
    public void EndPickPhase()
    {
        Toggle(bottomMenu);
        isDone = true;
        isPickOver = true;
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);

    }
    public void StartConfettiPhase()
    {
        print("Started confetti phase 1");
        confettiPhase = true;
        confettiSpawner.SetActive(true);
        cams.PackCamera();
        boxAnimate.Play("PresentGetIn");

    }
    public void StartConfettiPhaseTwo()
    {
        print("Started confetti phase 2");
        allowDrag = false;
        confettiEnabler.isConfetti = true;
        confettiPhase2 = true;
        confettiSpawner.SetActive(true);


    }
    public void EndConfettiPhase()
    {
        confettiPhase = false;
        allowDrag = true;
        confettiSpawner.SetActive(false);
    }
    public void EndConfettiPhaseTwo()
    {
        confettiPhase2 = false;
        allowDrag = true;
        confettiSpawner.SetActive(false);
        StartCardPhase();
    }
    public void StartCardPhase()
    {
        print("incardphase");
        card.transform.DOMove(card.transform.position + new Vector3(0, 0, 2.80f), 1.5f, false);

    }
    public void EndCardPhase()
    {
        P3DManager.GetComponent<P3dHitScreen>().enabled = true;
        EndPackPhase();
        StartStickerPhase();
    }
    public void StartStickerPhase()
    {
        stickersUI.SetActive(true);
        doneButton.SetActive(true);
        ObjectDestroyer();

    }
    public void EndStickerPhase()
    {
        P3DManager.GetComponent<P3dHitScreen>().enabled = false;
        stickersUI.SetActive(false);
        sendUI.SetActive(true);
        doneButton.SetActive(false);
    }

    public void StartPackPhase()
    {
        if (isPickOver)
        {

        }
    }
    public void EndPackPhase()
    {
        if (isPackOver)
        {

            boxAnimate.Play("PresentClose");
        }


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
    public void ObjectDestroyer()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Confetti");
        for (int i = 0; i < objectsToKill.Length; i++)
        {
            Destroy(objectsToKill[i],2f);
        }
        for (int i = 0; i < go.Length; i++)
        {
            Destroy(go[i],2f);
        }
    }



}
