using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using PaintIn3D;
using DG.Tweening;

public class GameFlow : MonoBehaviour
{
    private DrawerController dc;
    public SelectObject so;
    public GameObject ConfettiUI;
    [SerializeField] private AudioManager am;
    [Header("Animators")]
    [SerializeField] private Animator boxAnimate;
    [SerializeField] private Animator presentAnimate;

    [SerializeField] Unlockables unlock;
    [Header("UI's & GameObjects")]
    public GameObject topQuestMenu;
    public GameObject questBubble;
    public GameObject overlayUI;
    public GameObject youFailedUI;
    public GameObject startButton;
    public GameObject present;
    public GameObject endUI;
    public GameObject lid, box;

    public GameObject sendUI;
    public GameObject mainUI;
    public GameObject bottomMenu;
    public GameObject orderDialogue;
    public GameObject spritesObject;
    public GameObject doneButton;
    public bool cardPhase;

    public Sprite questionMark;
    public CameraPositions cams;
    [Header("Lists")]
    public List<Sprite> expectedOrder = new List<Sprite>();
    public List<string> currentOrder = new List<string>();
    public List<Sprite> allSprites = new List<Sprite>();
    [Header("Scripts etc")]
    [SerializeField] SliderScripts slider;
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
    public int starsTotal;
    [HideInInspector]
    public bool isPickOver, confettiPhase, confettiPhase2, isInAddingPhase, isPackOver, isAdded, isDone, hasStarted;
    public bool allowDrag;
    private bool endBoxPhase;

    [SerializeField] GameObject boxUI;
    [SerializeField] GameObject box_checkmark;
    [SerializeField] Animator box_heart_anim;
    [SerializeField] GameObject objectPositions, objectNewPositions;
    private void Start()
    {
        dc = GetComponent<DrawerController>();
    }
    void Awake()
    {
        Application.targetFrameRate = 60;
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.SetInt("currentLevel", 0);

        // PlayerPrefs.SetInt("currentLevel", 5);
        unlock.UnlockItems();
        Debug.Log("PlayerPrefs FILL value  is ------    " + PlayerPrefs.GetFloat("fill"));
        Debug.Log("Unlockables current level is --------   : " + Unlockables.currentLevel);
        Debug.Log("PlayerPrefs Level is ------    " + PlayerPrefs.GetInt("currentLevel"));
        Unlockables.currentLevel = PlayerPrefs.GetInt("currentLevel");
        Debug.Log("Unlockables current AFTER CHANGE IS  --------   : " + Unlockables.currentLevel);
        isPickOver = false;
        isPackOver = false;
        isAdded = false;
        isDone = false;
        hasStarted = false;

        GenerateQuest();
        // fill list of expected orders 
        foreach (Transform t in orderDialogue.transform)
        {
            expectedOrder.Add(t.GetComponent<Image>().sprite);
        }
        expectedOrder = expectedOrder.OrderBy(x => x.name).ToList();

        for (int i = 0; i < expectedOrder.Count; i++)
        {


        }
        // add all sprites to list
        foreach (Transform s in spritesObject.transform)
        {
            allSprites.Add(s.GetComponent<SpriteRenderer>().sprite);
        }
        //FillBottomMenu();
        // 



        // stickers scroll bar generator

        // foreach (Transform s in stickersObject.transform)
        // {
        //     GameObject obj = stickerUI_Image;
        //     Instantiate(obj, obj.transform.position, Quaternion.identity, stickersUI.transform);
        //     obj.transform.GetComponent<RawImage>().texture = s. GetComponent<Image>();
        //     obj.SetActive(true);
        // }
        confettiEnabler = confettiSpawner.GetComponent<DragObject>();
        P3DManager = GameObject.Find("P3DManager");
    }

    public void GenerateQuest()
    {

        for (int i = 0; i < 3; i++)
        {
            randoms[i] = Random.Range(1, spritesObject.transform.childCount - 1);
        }


        // for (int i = 0; i <= 2; i++)
        // {
        //     orderDialogue.transform.GetChild(i).GetComponent<Image>().sprite = spritesObject.transform.GetChild(randoms[i]).GetComponent<SpriteRenderer>().sprite;
        //     orderDialogue.transform.GetChild(i).GetComponent<Image>().SetNativeSize();
        // }
        int level = PlayerPrefs.GetInt("currentLevel");
        Debug.Log(level);
        switch (level)
        {

            case 0:
                orderDialogue.transform.GetChild(0).GetComponent<Image>().sprite = spritesObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(1).GetComponent<Image>().sprite = spritesObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(2).GetComponent<Image>().sprite = spritesObject.transform.GetChild(12).GetComponent<SpriteRenderer>().sprite;
                break;
            case 1:
                orderDialogue.transform.GetChild(0).GetComponent<Image>().sprite = spritesObject.transform.GetChild(6).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(1).GetComponent<Image>().sprite = spritesObject.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(2).GetComponent<Image>().sprite = spritesObject.transform.GetChild(11).GetComponent<SpriteRenderer>().sprite;
                break;
            case 2:
                orderDialogue.transform.GetChild(0).GetComponent<Image>().sprite = spritesObject.transform.GetChild(18).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(1).GetComponent<Image>().sprite = spritesObject.transform.GetChild(8).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(2).GetComponent<Image>().sprite = spritesObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                break;
            case 3:
                orderDialogue.transform.GetChild(0).GetComponent<Image>().sprite = spritesObject.transform.GetChild(21).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(1).GetComponent<Image>().sprite = spritesObject.transform.GetChild(25).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(2).GetComponent<Image>().sprite = spritesObject.transform.GetChild(10).GetComponent<SpriteRenderer>().sprite;
                break;
            case 4:
                orderDialogue.transform.GetChild(0).GetComponent<Image>().sprite = spritesObject.transform.GetChild(14).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(1).GetComponent<Image>().sprite = spritesObject.transform.GetChild(18).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(2).GetComponent<Image>().sprite = spritesObject.transform.GetChild(7).GetComponent<SpriteRenderer>().sprite;
                break;
            case 5:
                orderDialogue.transform.GetChild(0).GetComponent<Image>().sprite = spritesObject.transform.GetChild(19).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(1).GetComponent<Image>().sprite = spritesObject.transform.GetChild(18).GetComponent<SpriteRenderer>().sprite;
                orderDialogue.transform.GetChild(2).GetComponent<Image>().sprite = spritesObject.transform.GetChild(5).GetComponent<SpriteRenderer>().sprite;
                break;
            default:
                for (int i = 0; i <= 2; i++)
                {
                    orderDialogue.transform.GetChild(i).GetComponent<Image>().sprite = spritesObject.transform.GetChild(randoms[i]).GetComponent<SpriteRenderer>().sprite;
                    orderDialogue.transform.GetChild(i).GetComponent<Image>().SetNativeSize();
                }
                break;

        }

        for (int i = 0; i <= 2; i++)
        {
            topQuestMenu.transform.GetChild(i).GetComponent<Image>().sprite = orderDialogue.transform.GetChild(i).GetComponent<Image>().sprite;
        }

    }


    public void StartLevel()
    {
        hasStarted = true;

        Toggle(startButton);
        //Toggle(bottomMenu);

        topQuestMenu.SetActive(true);
    }
    public void EndPickPhase()
    {
        //Toggle(bottomMenu);
        isDone = true;
        isPickOver = true;
        
        

    }
    public void StartBoxPhase()
    {
        endBoxPhase = true;
        cams.PackCamera();
        boxAnimate.Play("MoveIn");
        topQuestMenu.SetActive(false);
        boxUI.SetActive(true);
        box_checkmark.SetActive(true);
    }
    public void EndBoxPhase()
    {
        box_heart_anim.Play("OpenLids");
        endBoxPhase = false;
        boxUI.SetActive(false);
        box_checkmark.SetActive(false);
        StartConfettiPhase();
        dc.enabled = false;
    }
    public void StartConfettiPhase()
    {

        confettiPhase = true;
        confettiSpawner.SetActive(true);
        ConfettiUI.SetActive(true);

    }

    public void StartConfettiPhaseTwo()
    {

        allowDrag = false;
        confettiEnabler.isConfetti = true;
        confettiPhase2 = true;

        confettiSpawner.SetActive(true);
        ConfettiUI.SetActive(true);


    }
    public void EndConfettiPhase()
    {
        objectPositions.transform.DOMove(objectNewPositions.transform.position, 1f, false);
        confettiPhase = false;
        allowDrag = true;
        confettiSpawner.SetActive(false);
        ConfettiUI.SetActive(false);
    }
    public void EndConfettiPhaseTwo()
    {
        confettiPhase2 = false;
        allowDrag = true;
        confettiSpawner.SetActive(false);
        StartCardPhase();
        ConfettiUI.SetActive(false);
        cardPhase = true;
    }
    public void StartCardPhase()
    {


        card.transform.DOMove(card.transform.position + new Vector3(0, 0, 3.3725f), 1.5f, false);

    }
    public void EndCardPhase()
    {
        P3DManager.GetComponent<P3dHitScreen>().enabled = true;
        EndPackPhase();
        StartStickerPhase();
    }
    public void StartStickerPhase()
    {
        stickersUI.transform.parent.parent.gameObject.SetActive(true);
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
            box_heart_anim.Play("CloseLids");
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



        return total;
    }


    public int CalculateCoins()
    {
        int coins = 0;
        if (isPackOver)
        {

            coins = CheckOrder();
        }
        return coins;

    }

    public void StarsAchieved()
    {


        if (orders == 0)
        {
            Debug.Log("No star");
            Toggle(youFailedUI);
            am.PlayClip(3);
            starsTotal = 0;
            return;
        }
        else if (orders == 1) // 1 && orders < 3)
        {
            Debug.Log("One star");
            Toggle(endUI);

            stars.StarsScore(1);
            starsTotal = 1;
        }
        else if (orders == 2) // && orders < 5)
        {
            Debug.Log("Two stars");
            Toggle(endUI);

            stars.StarsScore(2);
            starsTotal = 2;
        }
        else
        {
            Debug.Log("Three stars");
            Toggle(endUI);

            stars.StarsScore(3);
            starsTotal = 3;
        }
        //unlock.UnlockItem();
        slider.FillSlider();
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
            if (objectsToKill[i].tag != "Card")
                Destroy(objectsToKill[i], 0.25f);
        }
        for (int i = 0; i < go.Length; i++)
        {
            //Destroy(go[i], 1.7f);
        }
    }





}
