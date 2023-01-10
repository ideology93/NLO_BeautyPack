using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockables : MonoBehaviour
{
    [SerializeField] List<GameObject> unlockableItems = new List<GameObject>();
    [SerializeField] GameObject sprites;

    public static int currentLevel = 0;
    [SerializeField] SliderScripts slider;
    int j;
    [SerializeField] public List<Sprite> spritesToUnlock = new List<Sprite>();


    private void Start()
    {

    }
    public void UnlockItems()
    {

        currentLevel = PlayerPrefs.GetInt("currentLevel");
        Debug.Log(PlayerPrefs.GetInt("currentLevel"));
        if (PlayerPrefs.GetInt("currentLevel") >= 0)
        {
            j = PlayerPrefs.GetInt("currentLevel");
            if ((j >= 5))
                j = 5;
            
            // kad dodjes kuci ako jos vise jede govna menjaj <=
            for (int i = 0; i <= j; i++)
            {
                Debug.Log("Unlocking :" + unlockableItems[i].transform.GetChild(0).name + "And index i is : " + i);
                unlockableItems[i].SetActive(true);
            }
        }
    }



}
