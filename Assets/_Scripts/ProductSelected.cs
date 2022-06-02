using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSelected : MonoBehaviour
{
    public GameObject spritesObject;
    public bool isSelected = false;
    public bool isAdded = false;
    public  Sprite sprite;

    void Start()
    {
        //find adequate sprite for the object to add to menu
        spritesObject = GameObject.Find("AllSprites");
        sprite = spritesObject.transform.Find(gameObject.name).GetComponent<SpriteRenderer>().sprite;
    }
}
