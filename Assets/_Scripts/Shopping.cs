using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{
    [SerializeField] Material mat;

    [SerializeField] int index;
    [SerializeField] GameObject text;
    [SerializeField] GameObject coin;
    [SerializeField] int price;
    [SerializeField] GameObject table;
    [SerializeField] bool isUnlocked;
    [SerializeField] Sprite sprite;
    private void Start()
    {

    }
    public void BuyTable()
    {
        if (!isUnlocked)
        {
            if (PlayerStats.coins > price)
            {
                if(text!=null)
                text.SetActive(false);
                if(coin!=null)
                coin.SetActive(false);
                table.GetComponent<Renderer>().material = mat;
                isUnlocked = true;
                PlayerPrefs.SetInt("activeTable", index);
                PlayerStats.coins -= price;
                Debug.Log(gameObject.transform.GetComponent<Image>().sprite.name);
                gameObject.GetComponent<Image>().sprite = sprite;

            }
            else
            {
                Debug.Log("not enough money");
            }
        }
        else
        {
            PlayerPrefs.SetInt("activeTable", index);
            table.GetComponent<Renderer>().material = mat;  
        }
    }
}
