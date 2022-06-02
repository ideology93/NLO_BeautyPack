using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderScripts : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public GameObject lvlUI;
    public GameObject itemToUnlock;
    private float currentFill;


    private void Update()
    {

        if (slider.value >= 100)
        {
            LevelUP();
        }
        if (PlayerPrefs.GetInt("first") == 1 && !itemToUnlock.activeSelf)
        {
            itemToUnlock.SetActive(true);
        }
    }

    void Awake()
    {
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.wholeNumbers = true;
        float sliderValue = PlayerPrefs.GetFloat("fill");
        slider.value = sliderValue;

    }
    public void FillSlider(int a)
    {

        slider.value += a;
        currentFill = slider.value;
        PlayerPrefs.SetFloat("fill", currentFill);
    }
    public void LevelUP()
    {
        if (PlayerPrefs.GetInt("first") == 0)
        {
            PlayerPrefs.SetInt("first", 1);
            slider.value = 0;

            lvlUI.SetActive(true);
            itemToUnlock.SetActive(true);
            PlayerStats.myLevel++;
        }


    }
    public void CloseUI()
    {
        lvlUI.SetActive(false);
    }


}
