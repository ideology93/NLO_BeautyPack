using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectObject : MonoBehaviour
{
    public AudioManager am;
    [Header("Transforms & GameObjects")]
    public Transform positionOne;
    public Transform positionTwo;
    public Transform positionThree;
    public Transform positions;
    public Transform present;
    public GameObject dialogue;
    public GameObject sendUI;

    [HideInInspector]
    public int tableCount = 0;
    public int boxCount = 0;
    public int orderCount;
    private float timer = 1;

    [Header("Scripts and Animators")]
    public Animator anim;
    [SerializeField] private GameFlow flow;
    [SerializeField] private Present pres;






    void Awake()
    {
        orderCount = 0;
        tableCount = 0;
        boxCount = 0;
    }
    // Start is called before the first frame update    
    void Start()
    {
        orderCount = flow.expectedOrder.Count;
    }

    // Update is called once per frame    
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            AddSelectedObject();
        }
        if (flow.isPackOver)
        {   
            timer -= Time.deltaTime;
            if (timer <= 0 && timer >= -1)
            {
                timer = -10000;
                pres.Rotate();
            }
        }

    }
    public void AddSelectedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (tableCount < orderCount && flow.bottomMenu.transform.GetChild(tableCount).gameObject.activeSelf)
        {
            //take objects out of drawer
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Phone" && !EventSystem.current.IsPointerOverGameObject())
                {
                    flow.Toggle(dialogue);
                }

                if (hit.transform.tag == "Beauty" && !hit.transform.GetComponent<ProductSelected>().isSelected)
                {
                    am.PlayClip(0);
                    //move object to table with rotation and jump
                    StartCoroutine(Move(hit, tableCount));

                    //once object is selected, change isSelcted attribute to true
                    hit.transform.GetComponent<ProductSelected>().isSelected = true;
                    //change menu icon to selected object

                    flow.bottomMenu.transform.GetChild(tableCount).GetComponent<Image>().sprite = hit.transform.GetComponent<ProductSelected>().sprite;
                    Color temp = flow.bottomMenu.transform.GetChild(tableCount).GetComponent<Image>().color;
                    temp.a = 0.5f;
                    flow.bottomMenu.transform.GetChild(tableCount).GetComponent<Image>().color = temp;



                    //add name to the list
                    flow.currentOrder.Add(hit.transform.name);

                    hit.transform.SetParent(GameObject.Find("ObjectsOnTable").transform);

                    tableCount++;
                    if (orderCount == tableCount)
                    {
                        flow.EndPickPhase();
                        StartCoroutine(DelaySound());
                        flow.StartPackPhase();
                    }
                }
            }
        }
        //put objects from table into box
        else if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Beauty" && !hit.transform.GetComponent<ProductSelected>().isAdded)
            {
                am.PlayClip(4);
                hit.transform.SetParent(GameObject.Find("Present").transform);
                StartCoroutine(AddToBox(hit));
                boxCount++;
            }
            if (boxCount == tableCount)
            {
                flow.isPackOver = true;
            }
        }
    }

    public IEnumerator Move(RaycastHit hit, int tableCount)
    {


        if (!hit.transform.GetComponent<ProductSelected>().isSelected)
        {
            hit.transform.DOJump(hit.transform.position, 1, 1, 2, false);
            yield return new WaitForSeconds(0.5f);
            DOTween.To(() => hit.transform.position, x => hit.transform.position = x, positions.GetChild(tableCount).position, 2);
            hit.transform.GetComponent<ProductSelected>().isSelected = true;
        }


    }
    public IEnumerator AddToBox(RaycastHit hit)
    {

        Tween knockUpItem = hit.transform.DOJump(hit.transform.position, 0.1f, 0, 0.25f, false);
        yield return knockUpItem.WaitForCompletion();
        Tween moveItemToBox = DOTween.To(() => hit.transform.position, x => hit.transform.position = x, present.position + new Vector3(0, 2, 0), 0.75f);
        yield return moveItemToBox.WaitForCompletion();
        Tween moveLid = DOTween.To(() => hit.transform.position, x => hit.transform.position = x, present.position + new Vector3(0, 0.35f, 0), 0.75f);
        Tween rotato = hit.transform.DORotateQuaternion(hit.transform.rotation, 1.25f);
        Tween rotate = hit.transform.DOLocalRotate(new Vector3(320, 90, 320), 1.25f, RotateMode.FastBeyond360);
        hit.transform.GetComponent<ProductSelected>().isSelected = true;

    }
    public IEnumerator DelaySound()
    {
        yield return new WaitForSeconds(1.6f);
        am.PlayClip(5);
    }


}