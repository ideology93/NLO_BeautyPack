using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderScripts : MonoBehaviour
{
    public SceneFader sceneFader;
    public Slider slider;
    public Image fill;
    public GameObject lvlUI;
    public GameObject itemToUnlock;
    public GameObject winUI;
    private float currentFill;
    [SerializeField] GameObject sliderObj;

    [SerializeField] GameFlow flow;
    [SerializeField] Unlockables unlock;
    static float sliderValue;



    void Start()
    {

        slider.minValue = 0;
        slider.maxValue = 100;

        Debug.Log("Slider Value at Start is : " + slider.value);
        sliderValue = PlayerPrefs.GetFloat("fill");
        slider.value = sliderValue;
    }
    public void FillSlider()
    {
        StartCoroutine(FillBar());
    }


    public IEnumerator FillBar()
    {

        float star = flow.starsTotal;
        // Debug.Log("Stars: " + star);
        // float amountToFill = Mathf.Ceil(((star / 3) * 100));
        // Debug.Log("Amount to fill" + amountToFill);
        // Debug.Log("Current Slider Value : " + sliderValue);
        // float targetValue = sliderValue + amountToFill;
        // Debug.Log("Target : " + targetValue);
        // if (targetValue > 100)
        // {
        //     targetValue = 100;
        // }
        // PlayerPrefs.SetFloat("fill", targetValue);
        // for (float i = sliderValue; i <= targetValue; i += 1)
        // {

        //     slider.value = i;
        //     if (slider.value >= 100)
        //     {
        //         Debug.Log("shouldveLeveled");
        //     }
        //     yield return new WaitForSeconds(0.01f);
        // }
        // Debug.Log("In The very fucking End Slider is " + sliderValue + " and in prefs its : " + PlayerPrefs.GetFloat("fill"));
        if (star > 0)
            LevelUP();
        yield return new WaitForSeconds(0.01f);


    }
    public void LevelUP()
    {
        Debug.Log("Level before level up: " + PlayerPrefs.GetInt("currentLevel"));
        if (PlayerPrefs.GetInt("first") == 0)
        {
            PlayerPrefs.SetInt("first", 1);
            PlayerStats.myLevel++;
        }
        slider.value = 0;
        PlayerPrefs.SetFloat("fill", 1);
        Unlockables.currentLevel++;
        PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel") + 1);
        Debug.Log("Level after level up: " + PlayerPrefs.GetInt("currentLevel"));



    }
    public void UnlockItemIfPossible()
    {
        if (PlayerPrefs.GetInt("currentLevel") >= 0 && PlayerPrefs.GetInt("currentLevel") <= 5)
        {
            lvlUI.SetActive(true);
            winUI.SetActive(false);
            itemToUnlock.GetComponent<Image>().sprite = unlock.spritesToUnlock[PlayerPrefs.GetInt("currentLevel")];
        }
        else
            sceneFader.Next();

    }



}
