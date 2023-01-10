using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private Text coinsCollected;
    [SerializeField] private TextMeshProUGUI coinsText;
    public static int coins;
    [HideInInspector]
    public int previousLevelCoins;
    [HideInInspector]
    public static int level = 1;
    [HideInInspector]
    public static int myLevel = 0;
    [HideInInspector]
    public int previousLevel;
    [Header("Scripts")]
    public GameFlow flow;

    void Start()
    {
        coins = PlayerPrefs.GetInt("coins", coins);
        //PlayerPrefs.DeleteAll();
        coinsText.text = coins.ToString();
        previousLevelCoins = coins;
        previousLevel = level;
    }

    public void AddCoins()
    {
        StartCoroutine(AnimateText());
    }



    IEnumerator AnimateText()
    {
        coinsCollected.text = "0";
        int gained = flow.CalculateCoins();
        int currentCoins = 0;
        coins += gained;
        coinsText.text = coins.ToString();

        yield return new WaitForSeconds(0.1f);
        while (currentCoins < gained)
        {

            currentCoins++;
            coinsCollected.text = currentCoins.ToString();

            yield return new WaitForSeconds(0.001f);
        }
        PlayerPrefs.SetInt("coins", coins);




    }

}
