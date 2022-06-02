using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject startUI;
    public GameObject shopButton;
    public GameFlow flow;
    public static bool wasActive;


    public void ShopUI()
    {
        flow.Toggle(shopUI);
        flow.Toggle(startUI);
        flow.Toggle(shopButton);
        if (flow.questBubble.activeSelf)
        {
            flow.Toggle(flow.questBubble);
            wasActive = true;
        }
        else if (wasActive)
        {
            flow.Toggle(flow.questBubble);
            wasActive = false;
        }
    }

}
