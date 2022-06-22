using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraPositions : MonoBehaviour
{


    [Header("UI & GameObjects")]
    public Transform table, leftDrawer, centerDrawer, rightDrawer, pack;
    public GameObject leftArrow, rightArrow, cameraOptions_1, cameraOptions_2, cameraOptions_3, testCanvas;
    private Transform[] positions;
    [HideInInspector]
    public int pos;
    public Transform[] cameras;
    void Start()
    {
        positions = new Transform[] { leftDrawer, centerDrawer, rightDrawer };
        pos = 1;
    }
    public void MoveToCenterDrawers()
    {
        DOTween.To(() => transform.position, x => transform.position = x, centerDrawer.position, 1);
        transform.DORotateQuaternion(centerDrawer.transform.rotation, 1);
    }

    public void ZoomInLeftDrawer()
    {
        DOTween.To(() => gameObject.transform.position, x => gameObject.transform.position = x, leftDrawer.transform.position, 1);
        transform.DORotateQuaternion(leftDrawer.transform.rotation, 1);
    }
    public void ZoomInRightDrawer()
    {
        DOTween.To(() => gameObject.transform.position, x => gameObject.transform.position = x, rightDrawer.transform.position, 1);
        transform.DORotateQuaternion(rightDrawer.transform.rotation, 1);
    }
    public void MoveToTable()
    {
        DOTween.To(() => gameObject.transform.position, x => gameObject.transform.position = x, table.transform.position, 0.75f);
        transform.DORotateQuaternion(table.transform.rotation, 0.75f);

    }
    public void MoveLeft()
    {

        rightArrow.SetActive(true);
        if (pos == 2)
        {
            MoveToCenterDrawers();
            pos--;
        }
        else if (pos == 1)
        {
            ZoomInLeftDrawer();
            pos--;
            leftArrow.SetActive(false);
        }
    }
    public void MoveRight()
    {

        leftArrow.SetActive(true);
        if (pos == 0)
        {
            MoveToCenterDrawers();
            pos++;
        }
        else if (pos == 1)
        {
            ZoomInRightDrawer();
            pos++;
            rightArrow.SetActive(false);
        }
    }
    public void SettingsCamer(int a)
    {
        switch (a)
        {
            case 0:
                cameraOptions_1.SetActive(false);
                cameraOptions_2.SetActive(false);
                cameraOptions_3.SetActive(false);
                testCanvas.SetActive(false);
                break;

            case 1:
                cameraOptions_1.SetActive(true);
                cameraOptions_2.SetActive(false);
                cameraOptions_3.SetActive(false);
                testCanvas.SetActive(true);
                break;

            case 2:
                cameraOptions_1.SetActive(false);
                cameraOptions_2.SetActive(true);
                cameraOptions_3.SetActive(false);
                testCanvas.SetActive(true);
                break;
            case 3:
                cameraOptions_1.SetActive(false);
                cameraOptions_2.SetActive(false);
                cameraOptions_3.SetActive(true);
                testCanvas.SetActive(true);
                break;
        }

    }
    public void MoveCamera(int pos)
    {
        DOTween.To(() => gameObject.transform.position, x => gameObject.transform.position = x, cameras[pos].transform.position, 0.75f);
        transform.DORotateQuaternion(cameras[pos].transform.rotation, 0.75f);
    }
    public void PackCamera()
    {
        DOTween.To(() => gameObject.transform.position, x => gameObject.transform.position = x, pack.transform.position, 0.75f);
        transform.DORotateQuaternion(pack.transform.rotation, 0.75f);
    }

}
