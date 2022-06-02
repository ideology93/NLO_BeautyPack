using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class SettingsMenu : MonoBehaviour
{


    public GameObject ui;
    public AudioSource audioSource;
    


    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }


}