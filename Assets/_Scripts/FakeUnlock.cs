using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeUnlock : MonoBehaviour
{
    public GameObject unlock;
    public GameObject icon;
    public Sprite sprite;

    public void SetUnlock()
    {
        unlock.SetActive(true);
        icon.GetComponent<Image>().sprite =  sprite;
    }
}
