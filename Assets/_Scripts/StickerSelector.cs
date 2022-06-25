using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PaintIn3D;

public class StickerSelector : MonoBehaviour
{
    private GameObject inboxTrigger;
    public GameObject P3DManager;
    void Start()
    {
        P3DManager = GameObject.Find("P3DManager");
        inboxTrigger = GameObject.Find("Triggers");
    }
    // Start is called before the first frame update
    public void SelectSticker()
    {
        P3dPaintDecal decal = P3DManager.GetComponent<P3dPaintDecal>();
        decal.Texture = null;
        Debug.Log("in selection");
        inboxTrigger.SetActive(false);
        print(decal.Texture);
        decal.Texture = GetComponent<Image>().mainTexture;
    }

}
