using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateDrawerScroll : MonoBehaviour
{
    [SerializeField] Transform right;
    [SerializeField] Transform left;
    [SerializeField] Transform start;


    public void StartScrolling()
    {
        StartCoroutine(NewTween());

    }


    IEnumerator NewTween()
    {
        yield return new WaitForSeconds(0.1f);
        Tween tw = transform.DOMove(start.position, 1.5f, false).SetEase(Ease.InOutSine);
        
        yield return new WaitForSeconds(1.52f);



    }
}
