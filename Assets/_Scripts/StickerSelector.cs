using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PaintIn3D;

public class StickerSelector : MonoBehaviour
{
    private GameObject inboxTrigger;
    public GameObject P3DManager;
    private P3dPaintDecal decal;
    void Start()
    {
        P3DManager = GameObject.Find("P3DManager");
        inboxTrigger = GameObject.Find("Triggers");
        decal = P3DManager.GetComponent<P3dPaintDecal>();
    }
    // Start is called before the first frame update
    public void SelectSticker()
    {

        decal.Texture = GetComponent<RawImage>().texture;
    }

}
