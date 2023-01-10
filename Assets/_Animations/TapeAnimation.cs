using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeAnimation : MonoBehaviour
{

    public List<SkinnedMeshRenderer> tapeList = new List<SkinnedMeshRenderer>();
    public Animator boxAnimation;
    public GameObject tape;
    public GameObject card;

    private void Start() {
        boxAnimation = GetComponent<Animator>();
    }
    public void TapeBox()
    {
        
        StartCoroutine(Taping());  
    }
    public IEnumerator Taping()
    {
        tape.SetActive(true);
        tape.GetComponent<Animator>().Play("Tape_Anim");
        for (int i = 0; i < tapeList.Count; i++)
        {
            tapeList[i].gameObject.SetActive(true);
            for (int j = 0; j <= 100; j+=10)
            {
                Debug.Log(j);
                tapeList[i].SetBlendShapeWeight(0, j);
                        yield return new WaitForSeconds(0.02f);

            }
            if(i != 2 && i != 4)
            tapeList[i].gameObject.SetActive(false);
            if(i == 2)
            yield return new WaitForSeconds(0.5f);
        }
    }
     public void DestroyCard()
    {
        Destroy(card);
    }

}
